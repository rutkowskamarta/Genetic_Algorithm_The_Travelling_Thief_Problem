using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticAlgorithmTTP
{
    //ZAWSZE BIORĘ NAJCENNIEJSZY PRZEDMIOT
    public class Thief
    {
        private DataLoaded dataLoaded = DataLoaded.GetInstance();
        public double currentVelocity { get; set; } = 1f;
        public int currentValueOfItems;
        public int currentWeightOfItems;
        public List<ItemElement> knapsack { get; set; }

        public Thief()
        {
            knapsack = new List<ItemElement>();
            currentValueOfItems = 0;
            currentWeightOfItems = 0;
            SetSpeedOfThief();
        }


        public void StealWhenInCity(CityElement city)
        {
            if(city.itemsInTheCity.Count!= 0)
            {
                ItemElement element = ChoosePerfectItem(city.itemsInTheCity);
                if (element != null)
                {
                    knapsack.Add(element);
                    SetParametersOfKnapsack(element);
                    SetSpeedOfThief();
                    //Console.WriteLine("Steal! hehe " + element.ToString());

                }
            }

        }

        private ItemElement ChoosePerfectItem(List<ItemElement> items)
        {
            items = items.OrderBy(i => i.profit).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                if(dataLoaded.capacityOfKnapsack - currentWeightOfItems > items[i].weight)
                {
                    return items[i];
                }
            }
            return null;
        }

        private void SetParametersOfKnapsack(ItemElement element)
        {
           
            currentValueOfItems += element.profit;
            currentWeightOfItems += element.weight;
        }

        private void SetSpeedOfThief()
        {
            currentVelocity = dataLoaded.maximumSpeed - currentWeightOfItems * (dataLoaded.maximumSpeed- dataLoaded.minimumSpeed)/dataLoaded.capacityOfKnapsack;

        }

      



    }
}
