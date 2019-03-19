using System;
using System.Collections.Generic;
using static System.Console;
using NAudio;
using NAudio.Wave;

namespace GeneticAlgorithmTTP
{
    class Program
    {

        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader();
            DataLoaded.GetInstance().FillTheDistancesMatrix();

            for (int i = 0; i < 10; i++)
            {
                Play();
            }
            PlayFinishSound();

            ReadLine();

        }

        private static void Play()
        {
            GeneticAlgorithm g = new GeneticAlgorithm();
            TSPSpecimen best = g.GeneticCycle();

            //TSPSpecimen best = new TSPSpecimen();
            WriteLine("BEST: " + best.objectiveFunction + " " + best.CitiesToString());
            Utilities.SavePathSolutionToFile(best, Utilities.CSV_SAVE_LOCATION_SOLUTION, Utilities.FILE_ANNOTATION_SOLUTION);
            Utilities.SaveKnapsackSolutionToFile(best, Utilities.CSV_SAVE_LOCATION_KNAPSACK_SOLUTION, Utilities.FILE_ANNOTATION_KNAPSACK_SOLUTION);
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
