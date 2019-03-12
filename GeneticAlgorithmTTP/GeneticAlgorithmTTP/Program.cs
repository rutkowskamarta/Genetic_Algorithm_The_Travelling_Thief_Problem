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

            GeneticAlgorithm g = new GeneticAlgorithm();
            TSPSpecimen best = g.GeneticCycle();

            WriteLine("BEST: " + best.objectiveFunction+ " "+best.CitiesToString());

            Utilities.SavePathSolutionToFile(best, Utilities.FILE_ANNOTATION_ROZWIAZANIE);
            WaveStream mainOutputStream = new WaveFileReader(Utilities.SOUND_FILE_NAME);
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

            WaveOutEvent player = new WaveOutEvent();

            player.Init(volumeStream);

            player.Play();
            ReadLine();
        }

      
    }
}
