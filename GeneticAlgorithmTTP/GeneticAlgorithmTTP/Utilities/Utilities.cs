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


        public const string FILE_NAME = "Data\\trivial_0.ttp";
        public const string SOUND_FILE_NAME = "sound.wav";
        public const string CSV_SAVE_LOCATION_FIRST = @"C:\Users\marar\Desktop\GAresults\Droga pierwsze pokolenie\";
        public const string CSV_SAVE_LOCATION_SOLUTION = @"C:\Users\marar\Desktop\GAresults\Droga rozwiązanie\";
        public const string CSV_SAVE_LOCATION_KNAPSACK_FIRST = @"C:\Users\marar\Desktop\GAresults\Plecak pierwsze pokolenie\";
        public const string CSV_SAVE_LOCATION_KNAPSACK_SOLUTION = @"C:\Users\marar\Desktop\GAresults\Plecak rozwiązanie\";
        public const string CSV_SAVE_LOCATION_STATISTICS = @"C:\Users\marar\Desktop\GAresults\Statystyki\";
        public const string CSV_FILE_EXTENSION = ".csv";
        public const string FILE_ANNOTATION_FIRST = "pierwsze";
        public const string FILE_ANNOTATION_SOLUTION = "rozwiazanie";
        public const string FILE_ANNOTATION_STATISTICS = "statystyka";
        public const string FILE_ANNOTATION_KNAPSACK_FIRST = "plecak_pierwsze";
        public const string FILE_ANNOTATION_KNAPSACK_SOLUTION = "plecak_rozwiazanie";

        public const double STAGNATION_FACTOR = 0.1; //w procentach
        public const int POPULATION_SIZE = 300;
        public const int NUMBER_OF_GENERATIONS = 320;
        public const double PROBABILITY_OF_CROSSOVER = 0.7;
        public const double PROBABILITY_OF_MUTATION = 0.01;
        public const int TOURNAMENT_SIZE = 5;

        public static void SavePathSolutionToFile(TSPSpecimen result, string filePath, string annotation)
        {
            using (var writer = new StreamWriter($"{filePath + DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}",true))
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
            }
        }

        public static void SaveKnapsackSolutionToFile(TSPSpecimen result, string filePath, string annotation)
        {
            using (var writer = new StreamWriter($"{filePath + DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteComment("PLECAK");
                csv.NextRecord();
                csv.WriteHeader<ItemElement>();
                csv.NextRecord();
                csv.WriteRecords(result.thief.knapsack);
            }
        }

        public static void SaveStatisticsToFile()
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION_STATISTICS + DateTime.Now.ToFileTime()}{FILE_ANNOTATION_STATISTICS}{CSV_FILE_EXTENSION}", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteComment("STATYSTYKI");
                csv.NextRecord();
                csv.WriteHeader<CsvResult>();
                csv.NextRecord();
                csv.WriteRecords(CsvStatisticsHolder.GetInstance().results);
            }
        }
    }
}
