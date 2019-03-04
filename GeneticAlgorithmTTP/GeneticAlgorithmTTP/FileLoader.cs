using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;
using System.Globalization;

namespace GeneticAlgorithmTTP
{
    class FileLoader
    {
        private string fileName;

        public FileLoader(string fileName)
        {
            this.fileName = fileName;
        }

        public DataLoaded LoadFile()
        {
            DataLoaded dataLoaded = new DataLoaded();
            try
            {
                StreamReader streamReader = new StreamReader(fileName);
                
                string wholeText = streamReader.ReadToEnd();
                string[] lines = wholeText.Split("\n");
                dataLoaded.problemName = lines[0].Split(' ')[2].Trim();
                dataLoaded.totalNumberOfCities = Int32.Parse(ExtractData(lines[2])[1]);

                dataLoaded.totalNumberOfItems = Int32.Parse(ExtractData(lines[3])[3]);
                dataLoaded.capacityOfKnapsack = Int32.Parse(ExtractData(lines[4])[3]);
                dataLoaded.minimumSpeed = Double.Parse(ExtractData(lines[5])[2].Replace('.',','));
                dataLoaded.maximumSpeed = Double.Parse(ExtractData(lines[6])[2].Replace('.',','));
                //dodać parsowanie danych do tablicy
                
                WriteLine(dataLoaded.ToString());
            }catch(IOException e)
            {
                WriteLine("Nie udało się odczytać pliku " + e);
            }
            catch(Exception e)
            {
                WriteLine("Parsing problem "+e);
            }
            return dataLoaded;
        }

        private string[] ExtractData(string line)
        {
            return System.Text.RegularExpressions.Regex.Split(line, @"\s+");
        }
        
    }
}
