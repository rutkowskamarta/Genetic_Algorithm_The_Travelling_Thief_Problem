using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    public class CityElement: ICloneable<CityElement>
    {
        public int index {get; set;}
        public double xCoordinate { get; set; }
        public double yCoordinate { get; set; }
        public List<ItemElement> itemsInTheCity { get; set; }

        public CityElement(int index, double xCoordinate, double yCoordinate)
        {
            this.index = index;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            itemsInTheCity = new List<ItemElement>();
        }

        public CityElement(int index, double xCoordinate, double yCoordinate, List<ItemElement> itemsInTheCity)
        {
            this.index = index;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.itemsInTheCity = itemsInTheCity;
        }
       
        public double CalculateDistance(CityElement other)
        {
            return Math.Sqrt(Math.Pow(xCoordinate - other.xCoordinate, 2) + Math.Pow(yCoordinate - other.yCoordinate, 2));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in itemsInTheCity)
                stringBuilder.Append(item.profit+" "+item.weight+" "+item.assignedNodeNumber);

            return $"{index} {xCoordinate} {yCoordinate} items: {stringBuilder.ToString()}";
        }

        public CityElement Clone()
        {
            if(itemsInTheCity.Count == 0)
                return new CityElement(index, xCoordinate, yCoordinate);

            return new CityElement(index, xCoordinate, yCoordinate, itemsInTheCity);
        }
    }
}
