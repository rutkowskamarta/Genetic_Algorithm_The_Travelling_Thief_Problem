using System;
using System.Collections.Generic;
using static System.Console;
using System.IO;
using CsvHelper;

namespace GeneticAlgorithmTTP
{
    class Program
    {

        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader(Utilities.FILE_NAME);

            DataLoaded.GetInstance().FillTheDistancesMatrix();

            GeneticAlgorithm g = new GeneticAlgorithm();
            TSPSpecimen best = g.GeneticCycle();
            WriteLine("BEST: " + best.TotalTimeOfTravel(g.thief.currentVelocity)+ " "+best.CitiesToString());

            Utilities.SaveSolutionToFile(best, "rozwiazanie");

            ReadLine();
        }

      
    }
}
