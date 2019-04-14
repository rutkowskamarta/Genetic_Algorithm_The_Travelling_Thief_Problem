using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticAlgorithmTTP
{
    class CsvStatisticsHolder
    {
        private static CsvStatisticsHolder instance = null;
        public List<CsvResult> results;

        private CsvStatisticsHolder()
        {
            results = new List<CsvResult>();
        }

        public static CsvStatisticsHolder GetInstance()
        {
            if(instance == null)
                instance = new CsvStatisticsHolder();

            return instance;
        }

        public void AddNewGenerationStatistics(int generationCounter, List<TSPSpecimen> generation)
        {
            double average = generation.Sum(p => p.objectiveFunction) / generation.Count;
            int bestSolution = generation.Max(p => p.objectiveFunction);
            int worstSolution = generation.Min(p => p.objectiveFunction);

            results.Add(new CsvResult(generationCounter, bestSolution, worstSolution, average));
        }

        public void Reset()
        {
            results.Clear();
        }
    }
}
