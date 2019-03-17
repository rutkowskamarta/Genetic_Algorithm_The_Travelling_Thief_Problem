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

        public int totalTravelTime { get; set; }

        public TSPSpecimen()
        {
            thief = new Thief();
            citiesVisitedInOrder = new List<CityElement>();
            //citiesVisitedInOrder.Add(dataLoaded.cities[4]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[2]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[6]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[3]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[9]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[1]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[5]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[0]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[7]);
            //citiesVisitedInOrder.Add(dataLoaded.cities[8]);

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
            thief.Reset();
            SetTotalTimeOfTravel();

            objectiveFunction = thief.currentValueOfItems - totalTravelTime;
            //Console.WriteLine(thief.currentValueOfItems + " "+totalTravelTime+" "+thief.currentVelocity);


        }

        private void SetTotalTimeOfTravel()
        {
            double totalTime = 0;
            for (int i = 0; i < citiesVisitedInOrder.Count-1; i++)
            {
                //Console.WriteLine("current city index: " + (citiesVisitedInOrder[i].index-1));
                //if(i!=0)
                //    Console.WriteLine("previous city index: " + (citiesVisitedInOrder[i-1].index - 1));
                //if(thief.knapsack.Count !=0)
                //    Console.WriteLine("after picking from a city: " + (thief.knapsack.Last().assignedNodeNumber-1));
                //Console.WriteLine("knapsack weight: " +  thief.currentWeightOfItems);
                //Console.WriteLine("knapsack profit: " +  thief.currentValueOfItems);
                //Console.WriteLine("current time travel from city to city: " + CalculateTime(thief.currentVelocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index));
                //Console.WriteLine("total time travelled: " + totalTime);
                //Console.WriteLine("====");


                if (i != 0)
                {
                    thief.StealWhenInCity(citiesVisitedInOrder[i]);
                }
                totalTime += CalculateTime(thief.currentVelocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index);
                //Console.WriteLine("next  TIME:" + CalculateTime(thief.currentVelocity, citiesVisitedInOrder[i].index, citiesVisitedInOrder[i + 1].index));

            }

            totalTime += CalculateTime(thief.currentVelocity, citiesVisitedInOrder[citiesVisitedInOrder.Count - 1].index, citiesVisitedInOrder[0].index);
            thief.StealWhenInCity(citiesVisitedInOrder[0]);
            //Console.WriteLine("TOTAL TIME:"+(int)Math.Ceiling(totalTime));
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
            newSpecimen.thief = thief.Clone();
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
