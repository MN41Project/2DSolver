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
            sys.Build(showDetails);

            int[] methods = new int[]{ LinearSystem.GAUSS_METHOD, LinearSystem.LU_METHOD, LinearSystem.THOMAS_METHOD };
            string[] methodsNames = new string[] { "Gauss method", "LU method", "Thomas method" };
            OptionSelector methodSelector = new OptionSelector()
                                                .WithOptions(methodsNames)
                                                .WithExplanation("Choose a method");
            
            OptionSelector resultsTypeSelector = new OptionSelector()
                                                    .WithOptions(new string[]{"No", "Yes"})
                                                    .WithExplanation("Do you want the results in a file inside the Results folder?");
            bool wantResultsInFile = resultsTypeSelector.PickAnOption() == 1;

            try {
                Vector results = sys.SimpleSystem.Solve(methods[methodSelector.PickAnOption()], showDetails);

                if (!wantResultsInFile)
                {
                    results.Display("", sys.SimpleSystem.UnknownsNames);
                }
                else
                {
                    string resultsFileName = "results_" + file.Name;
                    ResultsFile resultsFile = new ResultsFile()
                        .WithPath(@"../../../Results/" + resultsFileName).WithDiscreteSystem(sys);
                    resultsFile.Write(showDetails);
                    Console.WriteLine("\n{0} has been correctly written inside Results/", resultsFileName);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
		}
	}
}
