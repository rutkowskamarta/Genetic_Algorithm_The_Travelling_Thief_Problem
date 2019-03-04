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

        //i jeszcze tablica 
        public override string ToString()
        {
            return problemName + " " + totalNumberOfCities + " " + totalNumberOfItems + " " + capacityOfKnapsack + " " + maximumSpeed + " " + minimumSpeed;
        }
    }
}
