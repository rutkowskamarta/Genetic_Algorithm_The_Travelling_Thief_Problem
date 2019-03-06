using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;
using static GeneticAlgorithmTTP.Utilities;

namespace GeneticAlgorithmTTP
{
    class GeneticAlgorithm
    {
        private Thief thief;

        private int populationSize;
        private int numberOfGenerations; //stop condition - może to być warunek stopu
        private double probabilityOfCrossover;
        private double probabilityOfMutation;
        private int tour; //rozmiar turnieju 

        private delegate TSPSpecimen mutationDelegate(TSPSpecimen specimen);

        private mutationDelegate mutationFunction;

        private SELECTION_METHOD selectionMethod = SELECTION_METHOD.RULETKA;
        private MUTATION_METHOD mutationMethod = MUTATION_METHOD.SWAP;

        public MUTATION_METHOD MutationMethod
        {
            get
            {
                return mutationMethod;
            }
            set
            {
                mutationMethod = value;
                switch (value)
                {
                    case MUTATION_METHOD.INVERSE:
                        mutationFunction = InverseMutation;
                        break;
                    case MUTATION_METHOD.SWAP:
                        mutationFunction = SwapMutation;
                        break;
                }
            }
        }

        private TSPSpecimen testRandomSpecimen;

        Random random;

        public GeneticAlgorithm(DataLoaded dataLoaded)
        {
            thief = new Thief();
            random = new Random();
            testRandomSpecimen = new TSPSpecimen(dataLoaded);

            MutationMethod = mutationMethod;
        }

        private TSPSpecimen Mutate(TSPSpecimen specimen)
        {
            double chance = random.NextDouble();
            if (chance <= probabilityOfMutation)
            {
                return mutationFunction(specimen);
            }
            else
            {
                return specimen;
            }
        }

        #region MUTATION_FUNCTIONS

        private TSPSpecimen SwapMutation(TSPSpecimen specimen)
        {
            int index1 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            int index2 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            while (index2 == index1)
            {
                index2 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            }
            CityElement city1 = specimen.citiesVisitedInOrder[index1];
            CityElement city2 = specimen.citiesVisitedInOrder[index2];

            specimen.citiesVisitedInOrder[index1] = city2;
            specimen.citiesVisitedInOrder[index2] = city1;
            return specimen;
        }

        private TSPSpecimen InverseMutation(TSPSpecimen specimen)
        {
            int index1 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            int index2 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            while (index2 == index1)
            {
                index2 = random.Next(0, specimen.citiesVisitedInOrder.Count);
            }

            if (index2 < index1)
            {
                int temp = index2;
                index2 = index1;
                index1 = temp;
            }

            List<CityElement> citiesInRange = new List<CityElement>();

            for (int i = index1; i < index2; i++)
            {
                citiesInRange.Add(specimen.citiesVisitedInOrder[i]);
            }

            citiesInRange = citiesInRange.OrderBy(a => Guid.NewGuid()).ToList();
            for (int i = index1; i < index2; i++)
            {
                specimen.citiesVisitedInOrder[i] = citiesInRange[i];
            }
            return specimen;
        }

        #endregion

        #region CROSS_FUNCTIONS
        //jedno dziecko powstaje w moim alg
        private TSPSpecimen Cross(TSPSpecimen parent1, TSPSpecimen parent2)
        {
            double chance = random.NextDouble();
            if (chance <= probabilityOfCrossover)
            {
                return OXCross(parent1, parent2);
            }
            else
            {
                return parent1;
            }
        }

        private TSPSpecimen OXCross(TSPSpecimen parent1, TSPSpecimen parent2)
        {
            int crossPoint1 = random.Next(0, parent1.citiesVisitedInOrder.Count);
            int crossPoint2 = random.Next(0, parent1.citiesVisitedInOrder.Count);
            while (crossPoint2 == crossPoint1)
            {
                crossPoint2 = random.Next(0, parent1.citiesVisitedInOrder.Count);
            }

            if (crossPoint2 < crossPoint1)
            {
                int temp = crossPoint2;
                crossPoint2 = crossPoint1;
                crossPoint1 = temp;
            }

            TSPSpecimen child = new TSPSpecimen();

            List<CityElement> crossSection = new List<CityElement>();
            
            for (int i = crossPoint1; i < crossPoint2; i++)
            {
                crossSection.Add(parent1.citiesVisitedInOrder[i]);
            }

            List<CityElement> restOfGenes = parent2.citiesVisitedInOrder.Where(p => !crossSection.Any(p2 => p2.index == p.index)).ToList<CityElement>();

            List<CityElement> childCities = new List<CityElement>();

            //część pierwsza - wstaw elementy z końca restgenes na początek child
            int part1Amount = parent1.citiesVisitedInOrder.Count - crossPoint2;
            childCities.AddRange(restOfGenes.Skip(part1Amount));
            restOfGenes.RemoveRange(part1Amount,restOfGenes.Count-part1Amount);
            childCities.AddRange(crossSection);
            childCities.AddRange(restOfGenes);
            child.citiesVisitedInOrder = childCities;
            return child;
        }
        #endregion
    }
}
