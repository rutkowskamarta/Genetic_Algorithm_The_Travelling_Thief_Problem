using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    class DataLoaded
    {
        public string problemName { get; set; }
        public int totalNumberOfCities { get; set; }
        public int totalNumberOfItems { get; set; }
        public int capacityOfKnapsack { get; set; }
        public double minimumSpeed { get; set; }
        public double maximumSpeed { get; set; }

        public CityElement firstCity { get; set; }
        public List<CityElement> cities { get; set; } //bez pierwszego miasta

        public double[,] distancesMatrix;

        public override string ToString()
        {
            return $"{problemName} {totalNumberOfCities} {totalNumberOfItems} {capacityOfKnapsack} {maximumSpeed} {minimumSpeed}";
        }

        public void FillTheDistancesMatrix()
        {
            distancesMatrix = new double[totalNumberOfCities, totalNumberOfCities];
            List<CityElement> calculationCities = new List<CityElement>();

            calculationCities.Add(firstCity);
            calculationCities.AddRange(cities);

           
            for (int i = 0; i < totalNumberOfCities; i++)
            {
                for (int j = i; j < totalNumberOfCities; j++)
                {
                    distancesMatrix[i, j] = calculationCities[i].CalculateDistance(calculationCities[j]);
                    distancesMatrix[j, i] = calculationCities[i].CalculateDistance(calculationCities[j]);
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
                    Console.Write(String.Format(format, (int)distancesMatrix[i, j]));
                }
                Console.WriteLine();
            }
        }
    }
}
