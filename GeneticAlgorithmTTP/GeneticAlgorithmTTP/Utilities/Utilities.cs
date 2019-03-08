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
            List<CityElement> resultList = new List<CityElement>();
            resultList.Add(result.firstCity);
            resultList.AddRange(result.citiesVisitedInOrder);
            using (var writer = new StreamWriter(@"C:\Users\marar\Desktop\GAresults\" + annotation+DateTime.Now.ToFileTime() + ".csv", true))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(resultList);
            }
        }
        

    }
}
