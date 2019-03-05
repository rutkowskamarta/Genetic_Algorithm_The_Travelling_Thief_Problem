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
        public ItemElement itemInTheCity { get; set; }

        public CityElement(int index, double xCoordinate, double yCoordinate)
        {
            this.index = index;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }

        public CityElement(int index, double xCoordinate, double yCoordinate, ItemElement itemElement)
        {
            this.index = index;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            itemInTheCity = itemElement;
        }

        public double CalculateDistance(CityElement other)
        {
            return Math.Sqrt(Math.Pow(xCoordinate - other.xCoordinate, 2) + Math.Pow(yCoordinate - other.yCoordinate, 2));
        }

        public override string ToString()
        {
            if (itemInTheCity != null)
                return $"{index} {xCoordinate} {yCoordinate} {itemInTheCity.ToString()}";
            else
                return $"{index} {xCoordinate} {yCoordinate} null";

        }

        public CityElement Clone()
        {
            if(itemInTheCity == null)
            {
                return new CityElement(index, xCoordinate, yCoordinate, null);

            }
            return new CityElement(index, xCoordinate, yCoordinate, itemInTheCity.Clone());
        }
    }
}
