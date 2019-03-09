using System;
using System.Collections.Generic;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    class Program
    {

        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader();
            DataLoaded.GetInstance().FillTheDistancesMatrix();

            GeneticAlgorithm g = new GeneticAlgorithm();
            TSPSpecimen best = g.GeneticCycle();

            WriteLine("BEST: " + best.objectiveFunction+ " "+best.CitiesToString());

            Utilities.SaveSolutionToFile(best, Utilities.FILE_ANNOTATION_ROZWIAZANIE);

            ReadLine();
        }

      
    }
}
