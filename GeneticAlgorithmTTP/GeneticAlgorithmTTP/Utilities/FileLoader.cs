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

        public FileLoader()
        {
            LoadFile();
        }

        public void LoadFile()
        {
            DataLoaded dataLoaded = DataLoaded.GetInstance();
            try
            {
                string[] allLines = File.ReadAllLines(Utilities.FILE_NAME);
                dataLoaded.problemName = allLines[0].Split(' ')[2].Trim();
                dataLoaded.totalNumberOfCities = int.Parse(ExtractData(allLines[2])[1]);

                dataLoaded.totalNumberOfItems = int.Parse(ExtractData(allLines[3])[3]);
                dataLoaded.capacityOfKnapsack = int.Parse(ExtractData(allLines[4])[3]);
                dataLoaded.minimumSpeed = double.Parse(ExtractData(allLines[5])[2], CultureInfo.InvariantCulture);
                dataLoaded.maximumSpeed = double.Parse(ExtractData(allLines[6])[2], CultureInfo.InvariantCulture);

                int counter = 10; //10 linijka to pierwsze dane o miastach
                string[] line = ExtractData(allLines[counter]);

                List<CityElement> allParsedCities = new List<CityElement>();

                line = ExtractData(allLines[counter]); 
                dataLoaded.cities = new List<CityElement>();

                while (line[0] != "ITEMS")
                {
                    dataLoaded.cities.Add(new CityElement(int.Parse(line[0]), double.Parse(line[1], CultureInfo.InvariantCulture), double.Parse(line[2], CultureInfo.InvariantCulture)));
                    counter++;
                    line = ExtractData(allLines[counter]);
                }

                while (counter<allLines.Length-1)
                {
                    counter++;
                    line = ExtractData(allLines[counter]);

                    ItemElement item = new ItemElement(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3]));
                    dataLoaded.cities[item.assignedNodeNumber-1].itemsInTheCity.Add(item);
                   
                }

                //foreach (var item in dataLoaded.cities)
                //{
                //    WriteLine(item.ToString());
                //}

            }catch(IOException e)
            {
                WriteLine("Nie udało się odczytać pliku " + e);
            }
            catch(Exception e)
            {
                WriteLine("Problem z parsowaniem "+e);
            }
        }

        private string[] ExtractData(string line)
        {
            return System.Text.RegularExpressions.Regex.Split(line, @"\s+");
        }
    }
}
