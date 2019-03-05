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
                string[] allLines = wholeText.Split("\n");
                dataLoaded.problemName = allLines[0].Split(' ')[2].Trim();
                dataLoaded.totalNumberOfCities = Int32.Parse(ExtractData(allLines[2])[1]);

                dataLoaded.totalNumberOfItems = Int32.Parse(ExtractData(allLines[3])[3]);
                dataLoaded.capacityOfKnapsack = Int32.Parse(ExtractData(allLines[4])[3]);
                dataLoaded.minimumSpeed = Double.Parse(ExtractData(allLines[5])[2].Replace('.',','));
                dataLoaded.maximumSpeed = Double.Parse(ExtractData(allLines[6])[2].Replace('.',','));

                int counter = 10; //10 linijka to pierwsze dane o miastach
                string[] line = ExtractData(allLines[counter]);
                dataLoaded.cities = new List<CityElement>();

                while (line[0] != "ITEMS")
                {
                    dataLoaded.cities.Add(new CityElement(Int32.Parse(line[0]), Double.Parse(line[1].Replace('.', ',')), Double.Parse(line[2].Replace('.', ','))));
                    counter++;
                    line = ExtractData(allLines[counter]);
                }
            
                counter++;
                line = ExtractData(allLines[counter]);

                while (counter<allLines.Length-1)
                {
                    ItemElement item = new ItemElement(Int32.Parse(line[0]), Int32.Parse(line[1]), Int32.Parse(line[2]), Int32.Parse(line[3]));
                    dataLoaded.cities[item.assignedNodeNumber - 1].itemInTheCity = item;
                    counter++;
                    line = ExtractData(allLines[counter]);
                }

            }catch(IOException e)
            {
                WriteLine("Nie udało się odczytać pliku " + e);
            }
            catch(Exception e)
            {
                WriteLine("Problem z parsowaniem "+e);
            }
            return dataLoaded;
        }

        private string[] ExtractData(string line)
        {
            return System.Text.RegularExpressions.Regex.Split(line, @"\s+");
        }

        
    }
}
