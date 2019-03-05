﻿using System;
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

        public List<CityElement> cities { get; set; }

        public double[,] distancesMatrix;

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

            //for (int i = 0; i < totalNumberOfCities; i++)
            //{
            //    for (int j = 0; j < totalNumberOfCities; j++)
            //    {
            //        const string format = "{0,-5}";
            //        Console.Write(String.Format(format, (int)distancesMatrix[i,j]) + " ");
            //    }
            //    Console.WriteLine();
            //}
        }



    }
}