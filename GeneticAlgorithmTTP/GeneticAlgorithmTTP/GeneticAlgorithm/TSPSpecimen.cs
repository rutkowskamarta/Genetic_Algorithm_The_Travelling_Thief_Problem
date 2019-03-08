using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    public class TSPSpecimen
    {
        public List<CityElement> citiesVisitedInOrder { get; set; }
        public double travelTime;
        private DataLoaded dataLoaded; 
        
        public TSPSpecimen(DataLoaded dataLoaded)
        {
            FillTheVisitedCitiesList();
        }

        public TSPSpecimen()
        {

        }

        private void FillTheVisitedCitiesList()
        {
            citiesVisitedInOrder = new List<CityElement>();

            for (int i = 0; i < dataLoaded.cities.Count; i++)
            {
                citiesVisitedInOrder.Add(dataLoaded.cities[i].Clone());
            }

            citiesVisitedInOrder = citiesVisitedInOrder.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public double TotalTimeOfTravel(float velocity)
        {
            double totalTime = 0;
            for (int i = 0; i < citiesVisitedInOrder.Count-1; i++)
            {
                totalTime += CalculateTime(velocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index);
            }
            totalTime += CalculateTime(velocity, citiesVisitedInOrder[citiesVisitedInOrder.Count - 1].index, citiesVisitedInOrder[0].index);
            return totalTime;
        }


        private double CalculateTime(float velocity, int sourceCityIdentifier, int destinationCityIdentifier)
        {
            return (dataLoaded.distancesMatrix[sourceCityIdentifier - 1, destinationCityIdentifier - 1])/velocity;
        }

        public TSPSpecimen Clone()
        {
            TSPSpecimen newSpecimen = new TSPSpecimen();
            newSpecimen.citiesVisitedInOrder = new List<CityElement>();
            newSpecimen.dataLoaded = dataLoaded;
            for (int i = 0; i < citiesVisitedInOrder.Count; i++)
            {
                newSpecimen.citiesVisitedInOrder.Add(citiesVisitedInOrder[i].Clone());
            }
            return newSpecimen;

        }

        public string CitiesToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in citiesVisitedInOrder)
            {
                s.Append(item.index+ ",");
            }
            return s.ToString();
        }

    }
}
