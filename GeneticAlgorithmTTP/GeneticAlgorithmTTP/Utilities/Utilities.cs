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


        public const string FILE_NAME = "Data\\trivial_1.ttp";
        public const string SOUND_FILE_NAME = "sound.wav";
        public const string CSV_SAVE_LOCATION = @"C:\Users\marar\Desktop\GAresults\";
        public const string CSV_FILE_EXTENSION = ".csv";
        public const string FILE_ANNOTATION_FIRST = "pierwsze";
        public const string FILE_ANNOTATION_ROZWIAZANIE = "rozwiazanie";

        public const double STAGNATION_FACTOR = 0.1; //w procentach
        public const int POPULATION_SIZE = 100;
        public const int NUMBER_OF_GENERATIONS = 1000;
        public const double PROBABILITY_OF_CROSSOVER = 0.7;
        public const double PROBABILITY_OF_MUTATION = 0.1;
        public const int TOURNAMENT_SIZE = 5;

        //public 

        public static void SavePathSolutionToFile(TSPSpecimen result, string annotation)
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION+DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}",true))
            using (var csv = new CsvWriter(writer))
            {

                List<CityElement> cities = new List<CityElement>();
                cities.AddRange(result.citiesVisitedInOrder);
                cities.Add(cities[0]);
                
                csv.WriteComment("MIASTA");
                csv.NextRecord();

                csv.WriteHeader<CityElement>();
                csv.NextRecord();
                csv.WriteRecords(cities);
                //csv.NextRecord();
                //csv.WriteComment("PLECAK");
                //csv.NextRecord();
                //csv.WriteHeader<ItemElement>();
                //csv.NextRecord();
                //csv.WriteRecords(result.thief.knapsack);
            }
        }

        public static void SaveKnapsackSolutionToFile(TSPSpecimen result, string annotation)
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION + DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteComment("PLECAK");
                csv.NextRecord();
                csv.WriteHeader<ItemElement>();
                csv.NextRecord();
                csv.WriteRecords(result.thief.knapsack);
            }
        }
    }
}
