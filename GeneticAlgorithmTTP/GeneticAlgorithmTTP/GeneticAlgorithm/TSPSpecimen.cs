using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    class TSPSpecimen
    {
        public List<CityElement> citiesVisitedInOrder { get; set; }
        private DataLoaded dataLoaded; 

        public TSPSpecimen(DataLoaded dataLoaded)
        {
            this.dataLoaded = dataLoaded;
            citiesVisitedInOrder = new List<CityElement>();
            FillTheVisitedCitiesList();
        }

        public TSPSpecimen()
        {

        }

        private void FillTheVisitedCitiesList()
        {
            citiesVisitedInOrder.Add(dataLoaded.firstCity.Clone());

            List<CityElement> restOfTheCities = new List<CityElement>();

            for (int i = 0; i < dataLoaded.cities.Count; i++)
            {
                restOfTheCities.Add(dataLoaded.cities[i].Clone());
            }

            restOfTheCities = restOfTheCities.OrderBy(a => Guid.NewGuid()).ToList();
            citiesVisitedInOrder.AddRange(restOfTheCities);
        }

        public double TotalTimeOfTravel(float velocity)
        {
            double totalTime = 0;
            for (int i = 0; i < citiesVisitedInOrder.Count-1; i++)
            {
                totalTime += CalculateTime(velocity, i, i + 1);
            }
            totalTime += CalculateTime(velocity, citiesVisitedInOrder.Count - 1, 1);
            return totalTime;
        }


        private double CalculateTime(float velocity, int sourceCityInList, int destinationCityInList)
        {
            //sourceCityInList oraz destinationCityInList to nie ich identyfikatory a indeks w liście citiesVisitedInOrder
            return (dataLoaded.distancesMatrix[citiesVisitedInOrder[sourceCityInList].index-1, citiesVisitedInOrder[destinationCityInList].index - 1])/velocity;
        }

    }
}
