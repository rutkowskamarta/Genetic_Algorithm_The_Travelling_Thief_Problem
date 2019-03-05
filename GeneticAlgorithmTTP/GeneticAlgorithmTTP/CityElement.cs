using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    class CityElement
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
    }
}
