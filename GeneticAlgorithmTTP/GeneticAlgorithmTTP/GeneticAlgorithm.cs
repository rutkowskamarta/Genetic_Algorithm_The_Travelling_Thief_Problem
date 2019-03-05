using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    class GeneticAlgorithm
    {
        private int populationSize;
        private int numberOfGenerations; //stop condition - może to być warunek stopu
        private double probabilityOfCrossover;
        private double probabilityOfMutation;
        private int tour; //??? 

        private string selectionMethod; //ruletka, turniej, ranking
        private string crossOverMethod; //OX, CX, PMX

    }
}
