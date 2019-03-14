using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    class CsvResult
    {
        public int generationCounter { get; }
        public int bestResult { get; }
        public int worstResult { get; }
        public double averageOfResults { get; }

        public CsvResult(int generationCounter, int bestResult, int worstResult, double averageOfResults)
        {
            this.generationCounter = generationCounter;
            this.bestResult = bestResult;
            this.worstResult = worstResult;
            this.averageOfResults = averageOfResults;
        }
    }
}
