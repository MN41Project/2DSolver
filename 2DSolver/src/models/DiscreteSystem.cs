using System;
using System.Collections.Generic;
using System.Linq;

namespace DSolver
{
    public class DiscreteSystem
    {

        public List<Element> Elements { get; private set; }
        public SquareMatrix AssembledMatrix { get; private set; }
        public Vector SecondMember { get; private set; }
        public int NodesCount { get; private set; }
        public LinearSystem SimpleSystem { get; private set; }

        private bool[] IsUnknown { get; set; }
        private double[] BCValues { get; set; }

        public DiscreteSystem()
        {
            this.Elements = new List<Element>();
        }

        public DiscreteSystem WithNodesCount(int count)
        {
            this.NodesCount = count;
            return this;
        }

        public DiscreteSystem WithElements(List<Element> elements)
        {
            this.Elements = elements;
            return this;
        }

        public DiscreteSystem AddElement(Element e)
        {
            this.Elements.Add(e);
            return this;
        }

        public void Build(bool showDetails)
        {
            this.FillAssembledMatrix();
            this.FillSecondMember();
            this.SimplifySystem();

            if (showDetails)
            {
                this.AssembledMatrix.Display("Assembled matrix");
                this.SecondMember.Display("Second member");

                Console.WriteLine("BC-simplified system\n");
                this.SimpleSystem.Display();

                Utils.Pause();
            }
        }

        private void FillAssembledMatrix()
        {
            int size = this.NodesCount * 2;
            this.AssembledMatrix = new SquareMatrix().WithZeroes(size);
            foreach (Element element in this.Elements) {
                int posUi = 2 * element.FirstNode.Id;
                int posUj = 2 * element.SecondNode.Id;
                for (int i  = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i < 2 && j < 2)
                        {
                            this.AssembledMatrix.AddToValue(posUi + i, posUi + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i < 2 && j >= 2)
                        {
                            this.AssembledMatrix.AddToValue(posUi + i, posUj + j - 2, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i >= 2 && j < 2)
                        {
                            this.AssembledMatrix.AddToValue(posUj + i-2, posUi + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i >= 2 && j >= 2)
                        {
                            this.AssembledMatrix.AddToValue(posUj + i - 2, posUj + j - 2, element.ElementaryMatrix.GetValue(i, j));
                        }
                    }
                }
            }
        }

        private void FillSecondMember()
        {
            int size = this.NodesCount * 2;
            this.SecondMember = new Vector().WithZeroes(size);
            this.IsUnknown = new bool[size];
            this.BCValues = new double[size];
            for (int i = 0; i < this.NodesCount; i++)
            {
                this.IsUnknown[i] = true;   
            }
            foreach (Element element in this.Elements)
            {
                this.IsUnknown[2 * element.FirstNode.Id] = !element.FirstNode.BoundaryCondition.HasOnX;
                this.IsUnknown[2 * element.FirstNode.Id + 1] = !element.FirstNode.BoundaryCondition.HasOnY;
                this.IsUnknown[2 * element.SecondNode.Id] = !element.SecondNode.BoundaryCondition.HasOnX;
                this.IsUnknown[2 * element.SecondNode.Id + 1] = !element.SecondNode.BoundaryCondition.HasOnY;

                this.BCValues[2 * element.FirstNode.Id] =
                    element.FirstNode.BoundaryCondition.HasOnX ? element.FirstNode.BoundaryCondition.XValue : 0;
                this.BCValues[2 * element.FirstNode.Id + 1] =
                    element.FirstNode.BoundaryCondition.HasOnY ? element.FirstNode.BoundaryCondition.YValue : 0;
                this.BCValues[2 * element.SecondNode.Id] =
                    element.SecondNode.BoundaryCondition.HasOnX ? element.SecondNode.BoundaryCondition.XValue : 0;
                this.BCValues[2 * element.SecondNode.Id + 1] =
                    element.SecondNode.BoundaryCondition.HasOnY ? element.SecondNode.BoundaryCondition.YValue : 0;

                this.SecondMember.SetValue(2 * element.FirstNode.Id, element.FirstNode.Force.GetValue(0));
                this.SecondMember.SetValue(2 * element.FirstNode.Id + 1, element.FirstNode.Force.GetValue(1));
                this.SecondMember.SetValue(2 * element.SecondNode.Id, element.SecondNode.Force.GetValue(0));
                this.SecondMember.SetValue(2 * element.SecondNode.Id + 1, element.SecondNode.Force.GetValue(1));
            }
        }

        private List<string> GenerateUnknownsNames()
        {
            List<string> names = new List<string>();

            for (var i = 0; i < this.NodesCount; i++)
            {
                names.Add("u" + i);
                names.Add("v" + i);
            }

            return names;
        }

        private void SimplifySystem()
        {
            List<string> unknownsNames = this.GenerateUnknownsNames();

            int size = this.NodesCount * 2;
            int unknownsCount = size;
            for (int i = 0; i < this.IsUnknown.Length; i++)
            {
                if (!this.IsUnknown[i])
                {
                    unknownsCount--;
                }
            }

            List<Vector> vectors = new List<Vector>();
            vectors.AddRange(this.AssembledMatrix.toVectors());

            List<Vector> simplifiedVectors = new List<Vector>();
            List<Vector> toBeRemovedVectors = new List<Vector>(); 

            int namesRemovedCount = 0;

            for (int i = 0; i < size; i++)
            {
                Vector v = new Vector(unknownsCount);
                int count = 0;
                for (int j = 0; j < size; j++)
                {
                    if (this.IsUnknown[j]) {
                        v.SetValue(count, vectors[i].GetValue(j));
                        count++;
                    }
                }

                if (this.IsUnknown[i])
                {
                    simplifiedVectors.Add(v);
                }
                else
                {
                    unknownsNames.RemoveAt(i - namesRemovedCount);
                    namesRemovedCount++;

                    toBeRemovedVectors.Add(v * this.BCValues[i]);
                }
            }

            SquareMatrix simpleMatrix = new SquareMatrix().WithVectors(simplifiedVectors.ToArray().ToArray());

            Vector simpleSecondMember = new Vector(unknownsCount);

            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                if (this.IsUnknown[i])
                {
                    simpleSecondMember.SetValue(sum, this.SecondMember.GetValue(i));
                    sum++;
                }
            }

            for (int i = 0; i < toBeRemovedVectors.Count; i++)
            {
                simpleSecondMember = simpleSecondMember - toBeRemovedVectors[i];
            }

            this.SimpleSystem = new LinearSystem(simpleMatrix, simpleSecondMember).WithUnknownsNames(unknownsNames.ToArray());
        }
    }
}

