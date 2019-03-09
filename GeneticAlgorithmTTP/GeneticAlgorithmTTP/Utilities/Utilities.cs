using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.IO;

namespace GeneticAlgorithmTTP
{
    public static class Utilities
    {
        public enum SELECTION_METHOD { RULETKA, TURNIEJ, RANKING };
        public enum MUTATION_METHOD { SWAP, INVERSE };


        public const string FILE_NAME = "Data\\easy_0.ttp";
        public const string CSV_SAVE_LOCATION = @"C:\Users\marar\Desktop\GAresults\";
        public const string CSV_FILE_EXTENSION = ".csv";
        public const string FILE_ANNOTATION_FIRST = "pierwszepok";
        public const string FILE_ANNOTATION_ROZWIAZANIE = "rozwiazanie";

        public const double STAGNATION_FACTOR = 0.05; //w procentach
        public const int POPULATION_SIZE = 500;
        public const int NUMBER_OF_GENERATIONS = 30000;
        public const double PROBABILITY_OF_CROSSOVER = 0.7;
        public const double PROBABILITY_OF_MUTATION = 0.01;
        public const int TOUR = 5;

        public static void SaveSolutionToFile(TSPSpecimen result, string annotation)
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION+DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}",true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(result.citiesVisitedInOrder);
                csv.WriteRecords(result.thief.knapsack);
            }
        }
        

    }
}
