using System.Collections.Generic;
using static System.Console;
using System.Linq;
using NAudio.Wave;

namespace GeneticAlgorithmTTP
{
    class Program
    {
        static List<CsvTotalResult> results = new List<CsvTotalResult>();

        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader();
            DataLoaded.GetInstance().FillTheDistancesMatrix();

            
            RunGeneticAlgorithm();

            Utilities.SaveTotalsToFile(results);
            PlayFinishSound();
            WriteLine("==========KONIEC==========");
            ReadLine();

        }

        private static void RunGeneticAlgorithm()
        {
            CsvStatisticsHolder.GetInstance().Reset();
            GeneticAlgorithm g = new GeneticAlgorithm();
            TSPSpecimen best = g.GeneticCycle();

            WriteLine("FINISH: " + best.objectiveFunction);

            int b = best.objectiveFunction;
            double avg = CsvStatisticsHolder.GetInstance().results.Average(p => p.averageOfResults);
            double minavg = (CsvStatisticsHolder.GetInstance().results.Average(p => p.worstResult));
            double maxavg = (CsvStatisticsHolder.GetInstance().results.Average(p => p.bestResult));

            results.Add(new CsvTotalResult(b, maxavg, minavg, avg));
            Utilities.SaveStatisticsToFile();
        }

        private static void PlayFinishSound()
        {
            WaveStream mainOutputStream = new WaveFileReader(Utilities.SOUND_FILE_NAME);
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

            WaveOutEvent player = new WaveOutEvent();

            player.Init(volumeStream);

            player.Play();

        }
      
    }
}
