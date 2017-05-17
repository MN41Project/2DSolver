using System;
using System.Collections.Generic;

namespace DSolver
{
    public class DiscreteSystem
    {

        public List<Element> Elements { get; private set; }
        public SquareMatrix AssembledMatrix { get; private set; }
        public Vector SecondMember { get; private set; }
        public int NodesCount { get; private set; }

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
            this.FillAssembledMatrix();
            return this;
        }

        public DiscreteSystem AddElement(Element e)
        {
            this.Elements.Add(e);
            this.FillAssembledMatrix();
            return this;
        }

        private void FillAssembledMatrix()
        {
            int size = this.NodesCount * 2;
            this.AssembledMatrix = new SquareMatrix().WithZeroes(size);
            foreach (Element element in this.Elements) {
                int posUi = 2 * element.FirstNode.Id;
                int posUj = 2 * element.SecondNode.Id;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i < 2 && j < 2)
                        {
                            this.AssembledMatrix.SetValue(posUi + i, posUi + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i < 2 && j >= 2)
                        {
                            this.AssembledMatrix.SetValue(posUi + i, posUj + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i >= 2 && j < 2)
                        {
                            this.AssembledMatrix.SetValue(posUj + i, posUi + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                        else if (i >= 2 && j >= 2)
                        {
                            this.AssembledMatrix.SetValue(posUj + i, posUj + j, element.ElementaryMatrix.GetValue(i, j));
                        }
                            
                             
                    }
                }
            }
            this.AssembledMatrix.Display("Yo");
        }
    }
}

