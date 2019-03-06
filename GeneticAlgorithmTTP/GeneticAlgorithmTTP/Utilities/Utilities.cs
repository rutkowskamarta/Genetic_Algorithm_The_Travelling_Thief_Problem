using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    public static class Utilities
    {
        public const string FILE_NAME = "Data\\trivial_0.ttp";

        public const int POPULATION_SIZE = 100;
        public const int NUMBER_OF_GENERATIONS = 100;
        public const double PROBABILITY_OF_CROSSOVER = 0.7;
        public const double PROBABILITY_OF_MUTATION = 0.01;
        public const int TOUR = 5;

        public enum SELECTION_METHOD { RULETKA, TURNIEJ, RANKING };
        public enum CROSSOVER_METHOD { OX, CX, PMX };
        public enum MUTATION_METHOD { SWAP, INVERSE };
    }
}
