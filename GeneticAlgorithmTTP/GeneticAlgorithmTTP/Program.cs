using System;
using static System.Console;
using System.IO;

namespace GeneticAlgorithmTTP
{
    class Program
    {
        private const string FILE_NAME = "Data\\trivial_0.ttp";

        private const string RULETKA_SELECTION_METHOD = "ruletka";
        private const string TURNIEJ_SELECTION_METHOD = "turniej";
        private const string RANKING_SELECTION_METHOD = "ranking";

        private const string OX_CROSSOVER_METHOD = "ox";
        private const string CX_CROSSOVER_METHOD = "cx";
        private const string PMX_CROSSOVER_METHOD = "pmx";

        private const int POPULATION_SIZE = 100;
        private const int NUMBER_OF_GENERATIONS = 100; 
        private const double PROBABILITY_OF_CROSSOVER = 0.7;
        private const double PROBABILITY_OF_MUTATION = 0.01;
        private const int TOUR = 5; 

        private static DataLoaded dataLoaded;


        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader(FILE_NAME);
            dataLoaded = fileLoader.LoadFile();
            dataLoaded.FillTheDistancesMatrix();

            GeneticAlgorithm g = new GeneticAlgorithm(dataLoaded);

            ReadLine();
        }

      
    }
}
