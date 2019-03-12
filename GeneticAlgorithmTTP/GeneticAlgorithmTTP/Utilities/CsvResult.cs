using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmTTP
{
    class CsvResult
    {
        public int generationCounter { get; set; }
        public int bestResult { get; set; }
        public int worstResult { get; set; }
        public double averageOfResults { get; set; }
    }
}
