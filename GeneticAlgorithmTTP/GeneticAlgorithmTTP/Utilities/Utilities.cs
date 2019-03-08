using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.IO;

namespace GeneticAlgorithmTTP
{
    public static class Utilities
    {
        public const string FILE_NAME = "Data\\trivial_0.ttp";
        public const string CSV_SAVE_LOCATION = @"C:\Users\marar\Desktop\GAresults\";
        public const string CSV_FILE_EXTENSION = ".csv";

        public const int POPULATION_SIZE = 100;
        public const int NUMBER_OF_GENERATIONS = 10000;
        public const double PROBABILITY_OF_CROSSOVER = 0.7;
        public const double PROBABILITY_OF_MUTATION = 0.1;
        public const int TOUR = 5;

        public enum SELECTION_METHOD { RULETKA, TURNIEJ, RANKING };
        public enum CROSSOVER_METHOD { OX, CX, PMX };
        public enum MUTATION_METHOD { SWAP, INVERSE };

        public static void SaveSolutionToFile(TSPSpecimen result, string annotation)
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION+DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}",true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(result.citiesVisitedInOrder);
            }
        }
        

    }
}
