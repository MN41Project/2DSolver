using System;

namespace DSolver
{
	class MainClass
	{
		public static void Main (string[] args)
		{

            /*
             * MANIPULATION DE MATRICES
             */

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
             */

            Console.WriteLine("\nVECTEURS\n====================\n");

            // Déclaration d'un vecteur
			Vector vector = new Vector().WithValues(new double[3]{1, 2, 2});
			Vector vectorZ = new Vector().WithZeroes(4);

            // Affichage sans nom
			vector.Display();

            // Changement d'une valeur (position, nouvelleValeur)
			vector.SetValue(2, 12);
            vector.Display();

            // Affichage avec nom
			vectorZ.Display("Zeros");


            /*
             * SYSTEMES LINEAIRES
             */

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

			Console.WriteLine("\nNODES ET ELEMENTS\n====================\n");

			Node node1 = new Node().WithId(1).WithPosX(0).WithPosY(0).WithForce(new Vector(new double[2]{3, 3}));
			Node node2 = new Node().WithId(2).WithPosX(2).WithPosY(2).WithForce(new Vector(new double[2]{0, 0}));

			Element element = new Element().WithType(Element.ELASTIC_BEAM)
								.WithFirstNode(node1)
								.WithSecondNode(node2)
								.WithYoungModulus(210000)
								.WithSection(100);
			
			element.ElementaryMatrix.Display("Elementary matrix");

            Console.ReadKey();
		}
	}
}
