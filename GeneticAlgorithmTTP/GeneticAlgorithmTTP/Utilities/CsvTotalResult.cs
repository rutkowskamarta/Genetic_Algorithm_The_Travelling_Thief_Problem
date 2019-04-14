using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    public class CsvTotalResult
    {

        public int bestResult { get; }
        public double bestResultAverage { get; }
        public double worstResultAverage { get; }
        public double averageOfResultsAverage { get; }

        public CsvTotalResult(int bestResult, double bestResultAverage, double worstResultAverage, double averageOfResultsAverage)
        {
            this.bestResult = bestResult;
            this.bestResultAverage = bestResultAverage;
            this.worstResultAverage = worstResultAverage;
            this.averageOfResultsAverage = averageOfResultsAverage;
        }

    }
}
