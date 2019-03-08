using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    public class TSPSpecimen
    {
        public CityElement firstCity { get; set; }
        public List<CityElement> citiesVisitedInOrder { get; set; } //bez pierwszego
        public double travelTime;
        private DataLoaded dataLoaded; 
        
        public TSPSpecimen(DataLoaded dataLoaded)
        {
            this.dataLoaded = dataLoaded;
            FillTheVisitedCitiesList();
        }

        public TSPSpecimen()
        {

        }

        private void FillTheVisitedCitiesList()
        {
            firstCity = dataLoaded.firstCity.Clone();

            citiesVisitedInOrder = new List<CityElement>();

            for (int i = 0; i < dataLoaded.cities.Count; i++)
            {
                citiesVisitedInOrder.Add(dataLoaded.cities[i].Clone());
            }

            citiesVisitedInOrder = citiesVisitedInOrder.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public double TotalTimeOfTravel(float velocity)
        {
            double totalTime = CalculateTime(velocity, firstCity.index, citiesVisitedInOrder[0].index);
            for (int i = 0; i < citiesVisitedInOrder.Count-1; i++)
            {
                totalTime += CalculateTime(velocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index);
            }
            totalTime += CalculateTime(velocity, citiesVisitedInOrder[citiesVisitedInOrder.Count - 1].index, firstCity.index);
            return totalTime;
        }


        private double CalculateTime(float velocity, int sourceCityIdentifier, int destinationCityIdentifier)
        {
            //sourceCityInList oraz destinationCityInList to nie ich identyfikatory a indeks w liście citiesVisitedInOrder
            return (dataLoaded.distancesMatrix[sourceCityIdentifier - 1, destinationCityIdentifier - 1])/velocity;
        }

        public TSPSpecimen Clone()
        {
            TSPSpecimen newSpecimen = new TSPSpecimen();
            newSpecimen.firstCity = firstCity;
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
            s.Append(firstCity.index+",");
            foreach (var item in citiesVisitedInOrder)
            {
                s.Append(item.index+ ",");
            }
            return s.ToString();
        }

    }
}
