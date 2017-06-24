using System;
using System.IO;
using System.Collections.Generic;

namespace DSolver
{
    public class TxtFile
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public bool IsValid { get; private set; }

        private int NodesCount { get; set; }
        private List<Node> Nodes { get; set; }

        private int ElementsCount { get; set; }
        private List<Element> Elements { get; set; }

        public TxtFile()
        {
        }

        public TxtFile WithPath(string path)
        {
            this.Path = path;
            string[] parts = path.Split('/');
            this.Name = parts[parts.Length - 1];
            return this;
        }

        public void validateFile()
        {
            this.IsValid = false;
            StreamReader file = new StreamReader(File.OpenRead(this.Path));

            file.ReadLine();
            this.NodesCount = Convert.ToInt32(file.ReadLine());
            if (!(this.NodesCount > 0))
            {
                return;
            }
         
            file.ReadLine();
            this.Nodes = new List<Node>();
            for (var i = 0; i < NodesCount; i++)
            {
                double[] pos = Utils.LineToDoubles(file.ReadLine());
                if (pos.Length != 2)
                {
                    return;
                }
                this.Nodes.Add(new Node().WithId(i).WithPosX(pos[0]).WithPosY(pos[1]));
            }

            file.ReadLine();
            this.ElementsCount = Convert.ToInt32(file.ReadLine());
            if (!(this.ElementsCount > 0))
            {
                return;
            }

            file.ReadLine();
            this.Elements = new List<Element>();
            for (var i = 0; i < this.ElementsCount; i++)
            {
                int[] nodesIndexes = Utils.LineToInts(file.ReadLine());
                if (nodesIndexes.Length != 2)
                {
                    return;
                }
                this.Elements.Add(new Element()
                    .WithFirstNode(this.Nodes[nodesIndexes[0] - 1])
                    .WithSecondNode(this.Nodes[nodesIndexes[1] - 1])
                    .WithType(Element.ELASTIC_BEAM)
                    .WithYoungModulus(ElementAttributes.YoungModulus)
                    .WithSection(ElementAttributes.Section)
                );
            }

            file.ReadLine();

            for (var i = 0; i < this.NodesCount; i++)
            {
                double[] forces = Utils.LineToDoubles(file.ReadLine());
                if (forces.Length != 2)
                {
                    return;
                }
                this.Nodes[i].WithForce(new Vector().WithPolarValues(forces[0], forces[1]));
            }

            file.ReadLine();

            for (var i = 0; i < this.NodesCount; i++)
            {
                int[] bcs = Utils.LineToInts(file.ReadLine());
                if (bcs.Length != 4)
                {
                    return;
                }
                BoundaryCondition boundaryCondition = new BoundaryCondition();
                if (bcs[0] == 1)
                {
                    boundaryCondition.WithX(bcs[1]);
                }

                if (bcs[2] == 1)
                {
                    boundaryCondition.WithY(bcs[3]);
                }
                this.Nodes[i].WithBoundaryCondition(boundaryCondition);
            }

            this.IsValid = true;
        }

        public DiscreteSystem GetDiscreteSystem()
        {
            return new DiscreteSystem().WithNodesCount(this.NodesCount).WithElements(this.Elements);
        }
    }
}

