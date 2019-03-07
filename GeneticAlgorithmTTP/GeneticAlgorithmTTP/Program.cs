using System;
using static System.Console;
using System.IO;

namespace GeneticAlgorithmTTP
{
    class Program
    {
       

        private static DataLoaded dataLoaded;


        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader(Utilities.FILE_NAME);
            dataLoaded = fileLoader.LoadFile();
            dataLoaded.FillTheDistancesMatrix();

            GeneticAlgorithm g = new GeneticAlgorithm(dataLoaded);
            TSPSpecimen best = g.GeneticCycle();
            WriteLine("BEST: " + best.TotalTimeOfTravel(g.thief.currentVelocity));
            ReadLine();
        }

      
    }
}
