using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace GeneticAlgorithmTTP
{
    class GeneticAlgorithm
    {
        private Thief thief;

        private int populationSize;
        private int numberOfGenerations; //stop condition - może to być warunek stopu
        private double probabilityOfCrossover;
        private double probabilityOfMutation;
        private int tour; //rozmiar turnieju 

        private string selectionMethod; //ruletka, turniej, ranking
        private string crossOverMethod; //OX, CX, PMX

        private TSPSpecimen testRandomSpecimen;

        public GeneticAlgorithm(DataLoaded dataLoaded)
        {
            thief = new Thief();
            testRandomSpecimen = new TSPSpecimen(dataLoaded);
        }

        //tutaj będzie funkcja duże G
    }
}
