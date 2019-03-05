using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    public class ItemElement : ICloneable<ItemElement>
    {
        public int index { get; set; }
        public int profit { get; set; }
        public int weight { get; set; }
        public int assignedNodeNumber { get; set; }

        public ItemElement(int index, int profit, int weight, int node)
        {
            this.index = index;
            this.profit = profit;
            this.weight = weight;
            assignedNodeNumber = node;
        }

        public override string ToString()
        {
            return $"{index} {profit} {weight} {assignedNodeNumber}";
        }

        public ItemElement Clone()
        {
            return new ItemElement(index, profit, weight, assignedNodeNumber);
        }
    }
}
