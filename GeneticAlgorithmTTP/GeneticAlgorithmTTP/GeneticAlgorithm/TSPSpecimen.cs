using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    public class TSPSpecimen
    {

        private DataLoaded dataLoaded = DataLoaded.GetInstance();
        public Thief thief { get; set; }

        public List<CityElement> citiesVisitedInOrder { get; set; }
        public int objectiveFunction { get; set; }

        private int totalTravelTime { get; set; }

        public TSPSpecimen()
        {
            thief = new Thief();
            FillTheVisitedCitiesList();
            SetObjectiveFunction();
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

        public void SetObjectiveFunction()
        {
            SetTotalTimeOfTravel();
            objectiveFunction = thief.currentValueOfItems - totalTravelTime;
        }

        private void SetTotalTimeOfTravel()
        {
            double totalTime = 0;
            for (int i = 0; i < citiesVisitedInOrder.Count-1; i++)
            {
                totalTime += CalculateTime(thief.currentVelocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index);
                thief.StealWhenInCity(citiesVisitedInOrder[i]);
            }
            totalTime += CalculateTime(thief.currentVelocity, citiesVisitedInOrder[citiesVisitedInOrder.Count - 1].index, citiesVisitedInOrder[0].index);
            thief.StealWhenInCity(citiesVisitedInOrder[0]);

            totalTravelTime = (int)Math.Ceiling(totalTime);
        }

        private double CalculateTime(double velocity, int sourceCityIdentifier, int destinationCityIdentifier)
        {
            return (dataLoaded.distancesMatrix[sourceCityIdentifier - 1, destinationCityIdentifier - 1])/velocity;
        }

        public TSPSpecimen Clone()
        {
            TSPSpecimen newSpecimen = new TSPSpecimen();
            newSpecimen.citiesVisitedInOrder = new List<CityElement>();
            newSpecimen.totalTravelTime = totalTravelTime;
            newSpecimen.objectiveFunction = objectiveFunction;
            newSpecimen.thief = thief;
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
