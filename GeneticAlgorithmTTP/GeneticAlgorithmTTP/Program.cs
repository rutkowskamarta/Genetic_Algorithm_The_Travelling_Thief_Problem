using System;
using static System.Console;
using System.IO;

namespace GeneticAlgorithmTTP
{
    class Program
    {

        private static string fileName = "student\\trivial_0.ttp";
        private static DataLoaded dataLoaded;


        static void Main(string[] args)
        {
            FileLoader fileLoader = new FileLoader(fileName);
            fileLoader.LoadFile();
            ReadLine();
        }

      
    }
}
