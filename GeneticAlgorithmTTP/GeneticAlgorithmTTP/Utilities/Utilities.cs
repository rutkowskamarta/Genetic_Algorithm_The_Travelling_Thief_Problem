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

        public const string PROBLEM_NAME = "easy_0";
        public const string ADDITIONAL_PATH = "kpop hard4";
        public const string FILE_NAME = "Data\\"+PROBLEM_NAME+".ttp";
        public const string SOUND_FILE_NAME = "sound.wav";
        public const string CSV_SAVE_LOCATION_FIRST = @"C:\Users\marar\Desktop\GAresults\WYNIKI\"+ADDITIONAL_PATH+@"\Droga pierwsze pokolenie\";
        public const string CSV_SAVE_LOCATION_SOLUTION = @"C:\Users\marar\Desktop\GAresults\WYNIKI\" + ADDITIONAL_PATH + @"\Droga rozwiązanie\";
        public const string CSV_SAVE_LOCATION_KNAPSACK_FIRST = @"C:\Users\marar\Desktop\GAresults\WYNIKI\" + ADDITIONAL_PATH + @"\Plecak pierwsze pokolenie\";
        public const string CSV_SAVE_LOCATION_KNAPSACK_SOLUTION = @"C:\Users\marar\Desktop\GAresults\WYNIKI\" + ADDITIONAL_PATH + @"\Plecak rozwiązanie\";
        public const string CSV_SAVE_LOCATION_STATISTICS = @"C:\Users\marar\Desktop\GAresults\WYNIKI\" + ADDITIONAL_PATH + @"\";
        public const string CSV_FILE_EXTENSION = ".csv";
        public const string FILE_ANNOTATION_FIRST = "pierwsze";
        public const string FILE_ANNOTATION_SOLUTION = "rozwiazanie";
        public const string FILE_ANNOTATION_STATISTICS = "statystyka";
        public const string FILE_ANNOTATION_KNAPSACK_FIRST = "plecak_pierwsze";
        public const string FILE_ANNOTATION_KNAPSACK_SOLUTION = "plecak_rozwiazanie";

        public const double STAGNATION_FACTOR = 1; //w procentach
        public const int POPULATION_SIZE = 100;
        public const int NUMBER_OF_GENERATIONS = 100;
        public const double PROBABILITY_OF_CROSSOVER = 1;
        public const double PROBABILITY_OF_MUTATION = 0.2;
        public const int TOURNAMENT_SIZE = 100;

        public static void SavePathSolutionToFile(TSPSpecimen result, string filePath, string annotation)
        {
            
            using (var writer = new StreamWriter($"{filePath +PROBLEM_NAME+ DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}",true))
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
            using (var writer = new StreamWriter($"{filePath +PROBLEM_NAME+ DateTime.Now.ToFileTime()}{annotation}{CSV_FILE_EXTENSION}", true))
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
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION_STATISTICS +PROBLEM_NAME+ DateTime.Now.ToFileTime()}{FILE_ANNOTATION_STATISTICS}{CSV_FILE_EXTENSION}", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteComment("STATYSTYKI");
                csv.NextRecord();
                csv.WriteHeader<CsvResult>();
                csv.NextRecord();
                csv.WriteRecords(CsvStatisticsHolder.GetInstance().results);
            }
        }

        public static void SaveTotalsToFile(List<CsvTotalResult> totals)
        {
            using (var writer = new StreamWriter($"{CSV_SAVE_LOCATION_STATISTICS + PROBLEM_NAME +"TOTALS" + DateTime.Now.ToFileTime()}{FILE_ANNOTATION_STATISTICS}{CSV_FILE_EXTENSION}", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteComment("STATYSTYKI");
                csv.NextRecord();
                csv.WriteHeader<CsvResult>();
                csv.NextRecord();
                csv.WriteRecords(totals);
            }
        }

    
       
    }
}
