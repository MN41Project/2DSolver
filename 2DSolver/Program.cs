using System;
using System.Collections.Generic;
namespace DSolver
{
	class MainClass
	{
        public static void Main(string[] args)
        {
            bool showDetails = false;
            OptionSelector verboseSelector = new OptionSelector()
                                                .WithOptions(new string[]{ "Without details", "With details" })
                                                .WithExplanation("Choose a verbose level");
            showDetails = verboseSelector.PickAnOption() == 1;

            FilePicker fp = new FilePicker().WithBasePath(@"../../../Data");
            TxtFile file = fp.PickAFile();

            if (!file.IsValid)
            {
                Console.WriteLine("This file can't be used");
                return;
            }

            DiscreteSystem sys = file.GetDiscreteSystem();
            sys.AssembledMatrix.Display();
            sys.SecondMember.Display();
            sys.SimpleSystem.Display();

            int[] methods = new int[]{ LinearSystem.GAUSS_METHOD, LinearSystem.LU_METHOD, LinearSystem.THOMAS_METHOD };
            string[] methodsNames = new string[] { "Gauss method", "LU method", "Thomas method" };
            OptionSelector methodSelector = new OptionSelector()
                                                .WithOptions(methodsNames)
                                                .WithExplanation("Choose a method");
            
            sys.SimpleSystem.Solve(methods[methodSelector.PickAnOption()], showDetails).Display("u");

            Console.ReadKey();
          
            /*
             * MANIPULATION DE MATRICES
             */
             /*
            Console.WriteLine("\nMATRICES\n====================\n");

            // Déclaration d'une matrice
            Matrix matrix = new Matrix().WithValues(new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            // Affichage sans nom
            matrix.Display();

            // Changement d'une valeur (ligne, colonne, nouvelleValeur)
            matrix.SetValue(2, 2, 1);
            matrix.Display();

            // Déclaration d'une matrice nulle
            matrix = new Matrix().WithZeroes(3, 2);

            // Affichage avec nom
            matrix.Display("MatriceNulle");

            // Ou en matrice carrée
            matrix = new SquareMatrix().WithZeroes(3);
            matrix.Display("CarréeNulle");


            /*
             * MANIPULATION DE VECTEURS
             

            Console.WriteLine("\nVECTEURS\n====================\n");

            // Déclaration d'un vecteur
            Vector vector = new Vector().WithValues(new double[3] { 1, 2, 2 });
            Vector vectorZ = new Vector().WithZeroes(4);

            // Affichage sans nom
            vector.Display();

            // Changement d'une valeur (position, nouvelleValeur)
            vector.SetValue(2, 12);
            vector.Display();

            // Affichage avec nom
            vectorZ.Display("Zeros");

            // Opérations
            Matrix m1 = new Matrix().WithValues(new double[2, 2] { { 1, 2 }, { 3, 4 } });
            Matrix m2 = new Matrix().WithOnes(2, 2);
            m1.Display();
            m2.Display();
            (m1 * -2 - 2 * m2).Display();
            Vector v1 = new Vector().WithValues(new double[] { 1, 2 });
            Vector v2 = new Vector().WithValues(new double[] { 3, 4 });
            (v1 + 2 * v2).Display();


            /*
             * SYSTEMES LINEAIRES
             

            Console.WriteLine("\nSYSTEMES\n====================\n");

            SquareMatrix K = new SquareMatrix(new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Vector F = new Vector(new double[3] { 1, 2, 3 });

            // Déclaration du système
            LinearSystem system = new LinearSystem(K, F);

            // Affichage du système
            system.Display();

            // Résolution du système (n'affiche rien)
            system.Solve();

            // Affichage de la solution précédemment calculée (la Solution est un objet de type Vector)
            system.Solution.Display("Solution");


            /*
             * NODES ET ELEMENTS
             */
            /*
           Console.WriteLine("\nNODES ET ELEMENTS\n====================\n");
           double a = 400, f=100, E=210000, A1=10, A2=20;
           Node node0 = new Node().WithId(0).WithPosX(0).WithPosY(0)
                           .WithBoundaryCondition(new BoundaryCondition().WithX(0).WithY(0));
           Node node1 = new Node().WithId(1).WithPosX(2*a).WithPosY(0)
                           .WithBoundaryCondition(new BoundaryCondition().WithY(0));
           Node node2 = new Node().WithId(2).WithPosX(a).WithPosY(a)
               .WithForce(new Vector(new double[2]{0, -f}));

           Element element1 = new Element().WithType(Element.ELASTIC_BEAM)
               .WithFirstNode(node0)
               .WithSecondNode(node1)
               .WithYoungModulus(E)
               .WithSection(A1);

           Element element2 = new Element().WithType(Element.ELASTIC_BEAM)
               .WithFirstNode(node1)
               .WithSecondNode(node2)
               .WithYoungModulus(E)
               .WithSection(A2);

           Element element3 = new Element().WithType(Element.ELASTIC_BEAM)
               .WithFirstNode(node2)
               .WithSecondNode(node0)
               .WithYoungModulus(E)
               .WithSection(A2);

           element1.ElementaryMatrix.Display("El 1");
           element2.ElementaryMatrix.Display("El 2");
           element3.ElementaryMatrix.Display("El 3");

           List<Element> elements = new List<Element>();
           elements.Add(element1);
           elements.Add(element2);
           elements.Add(element3);

           DiscreteSystem dSys = new DiscreteSystem().WithNodesCount(3).WithElements(elements);
           dSys.AssembledMatrix.Display("Matrice assemblee");
           dSys.SecondMember.Display("Second membre");
           dSys.SimpleSystem.Display();
           dSys.SimpleSystem.Solve().Display("Solution");
           */

            /* TEST SYSTEME ENONCE */
            /*
            double E = 1000000, d=0.015, s = Math.PI * d * d / 4;
            Node node1 = new Node().WithId(0).WithPosX(0).WithPosY(0.2)
                            .WithBoundaryCondition(new BoundaryCondition().WithX(0).WithY(0));
            Node node2 = new Node().WithId(1).WithPosX(0).WithPosY(0)
                            .WithBoundaryCondition(new BoundaryCondition().WithX(0).WithY(0));
            Node node3 = new Node().WithId(2).WithPosX(0.1).WithPosY(0.1);
            Node node4 = new Node().WithId(3).WithPosX(0.2).WithPosY(0.2);
            Node node5 = new Node().WithId(4).WithPosX(0.2).WithPosY(0);
            Node node6 = new Node().WithId(5).WithPosX(0.3).WithPosY(0.1).WithForce(new Vector().WithPolarValues(10, 315));
            Node node7 = new Node().WithId(6).WithPosX(0.4).WithPosY(0.2)
                            .WithBoundaryCondition(new BoundaryCondition().WithX(0).WithY(0));
            Node node8 = new Node().WithId(7).WithPosX(0.4).WithPosY(0)
                            .WithBoundaryCondition(new BoundaryCondition().WithX(0).WithY(0));
            List<Element> elements = new List<Element>();
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node1)
                .WithSecondNode(node4)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node1)
                .WithSecondNode(node3)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node3)
                .WithSecondNode(node4)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node2)
                .WithSecondNode(node3)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node3)
                .WithSecondNode(node5)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node2)
                .WithSecondNode(node5)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node4)
                .WithSecondNode(node7)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node4)
                .WithSecondNode(node6)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node6)
                .WithSecondNode(node7)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node5)
                .WithSecondNode(node6)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node6)
                .WithSecondNode(node8)
                .WithYoungModulus(E)
                .WithSection(s));
            elements.Add(new Element().WithType(Element.ELASTIC_BEAM)
                .WithFirstNode(node5)
                .WithSecondNode(node8)
                .WithYoungModulus(E)
                .WithSection(s));
            DiscreteSystem sys = new DiscreteSystem().WithNodesCount(8).WithElements(elements);
            sys.AssembledMatrix.Display();
            sys.SecondMember.Display();
            sys.SimpleSystem.Display();
            sys.SimpleSystem.Solve().Display();
            Console.ReadKey();
*/
            //Console.SetWindowSize(800, 800);
		}
	}
}
