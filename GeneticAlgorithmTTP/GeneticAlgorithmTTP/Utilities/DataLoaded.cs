using System;
using System.Collections.Generic;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    public sealed class DataLoaded
    {
        private static DataLoaded instance = null;

        public string problemName { get; set; }
        public int totalNumberOfCities { get; set; }
        public int totalNumberOfItems { get; set; }
        public int capacityOfKnapsack { get; set; }
        public double minimumSpeed { get; set; }
        public double maximumSpeed { get; set; }

        public List<CityElement> cities { get; set; } 

        public double[,] distancesMatrix;

        private DataLoaded(){}

        public static DataLoaded GetInstance()
        {
            if (instance == null)
                instance = new DataLoaded();

            return instance;
        }

        public override string ToString()
        {
            return $"{problemName} {totalNumberOfCities} {totalNumberOfItems} {capacityOfKnapsack} {maximumSpeed} {minimumSpeed}";
        }

        public void FillTheDistancesMatrix()
        {
            distancesMatrix = new double[totalNumberOfCities, totalNumberOfCities];

            for (int i = 0; i < totalNumberOfCities; i++)
            {
                for (int j = i; j < totalNumberOfCities; j++)
                {
                    distancesMatrix[i, j] = cities[i].CalculateDistance(cities[j]);
                    distancesMatrix[j, i] = cities[i].CalculateDistance(cities[j]);
                }
            }
        }

        public void WriteAllDistances()
        {
            for (int i = 0; i < totalNumberOfCities; i++)
            {
                for (int j = 0; j < totalNumberOfCities; j++)
                {
                    string format = "{0, -5}";
                    Write(String.Format(format, (int)distancesMatrix[i, j]));
                }
                WriteLine();
            }
        }
    }
}
