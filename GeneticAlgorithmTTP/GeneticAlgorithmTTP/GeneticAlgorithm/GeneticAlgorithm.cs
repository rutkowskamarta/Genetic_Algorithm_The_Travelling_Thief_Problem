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
        public Thief thief;

        private int currentGeneration = 0;

        private List<TSPSpecimen> population;
        private List<TSPSpecimen> newPopulation;

        private TSPSpecimen bestSolution;

        Random random;

        private delegate TSPSpecimen mutationDelegate(TSPSpecimen specimen);
        private mutationDelegate mutationFunction;

        private delegate List<TSPSpecimen> selectionDelegate();
        private selectionDelegate selectionFunction;

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

        public SELECTION_METHOD SelectionMethod
        {
            get
            {
                return selectionMethod;
            }
            set
            {
                selectionMethod = value;
                switch (value)
                {
                    case SELECTION_METHOD.RULETKA:
                        selectionFunction = RouletteSelectionMethod;
                        break;
                }
            }
        }


        public GeneticAlgorithm(DataLoaded dataLoaded)
        {
            thief = new Thief();
            random = new Random();

            MutationMethod = mutationMethod;
            SelectionMethod = selectionMethod;

            InitializePopulation(dataLoaded);
        }

        private void InitializePopulation(DataLoaded dataLoaded)
        {
            population = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                population.Add(new TSPSpecimen(dataLoaded));
            }
        }

        public TSPSpecimen GeneticCycle()
        {
            bestSolution = FindBest(population).Clone();
            Utilities.SaveSolutionToFile(bestSolution, "najzpierwszpokolenia");

            WriteLine("BEST: " + bestSolution.TotalTimeOfTravel(thief.currentVelocity) + " " + bestSolution.CitiesToString());

            while (currentGeneration < NUMBER_OF_GENERATIONS)
            {
                currentGeneration++;

                newPopulation = Selection();
                newPopulation.ForEach(p => Cross(p));
                newPopulation.ForEach(p => Mutate(p));

                TSPSpecimen bestSolutionInNewPopolation = FindBest(newPopulation);

                if (bestSolutionInNewPopolation.TotalTimeOfTravel(thief.currentVelocity) < bestSolution.TotalTimeOfTravel(thief.currentVelocity))
                {
                    bestSolution = bestSolutionInNewPopolation.Clone();
                }

                //    WriteLine("BEST: "+bestSolution.TotalTimeOfTravel(thief.currentVelocity)+" "+bestSolution.CitiesToString()
                //        +" new: "+bestSolutionInNewPopolation.TotalTimeOfTravel(thief.currentVelocity)+" "+bestSolutionInNewPopolation.CitiesToString());
            }

            return bestSolution;
        }

        private TSPSpecimen FindBest(List<TSPSpecimen> examinedPopulation)
        {
            return examinedPopulation.OrderBy(i1 => i1.TotalTimeOfTravel(thief.currentVelocity)).First();
        }

        private List<TSPSpecimen> Selection()
        {
            return selectionFunction();
        }

        private TSPSpecimen Mutate(TSPSpecimen specimen)
        {
            double chance = random.NextDouble();
            if (chance <= PROBABILITY_OF_MUTATION)
            {
                return mutationFunction(specimen);
            }
            else
            {
                return specimen;
            }
        }

        //jedno dziecko powstaje w moim alg
        private TSPSpecimen Cross(TSPSpecimen self)
        {
            double chance = random.NextDouble();
            if (chance <= PROBABILITY_OF_CROSSOVER)
            {
                int index = random.Next(POPULATION_SIZE);
                TSPSpecimen secondParent = newPopulation[index];
                return OXCross(self, secondParent);
            }
            else
            {
                return self;
            }
        }

        //wyliczenie dla wszystkich osobników miary
        private void Evaluate()
        {

        }

        #region SELECTION_METHODS
        private List<TSPSpecimen> RouletteSelectionMethod()
        {
            double totalFitnessSum = population.Sum(specimen => specimen.TotalTimeOfTravel(thief.currentVelocity));
            List<TSPSpecimen> newPopulation = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                newPopulation.Add(RouletteSelectionMethodForOneSpecimen(totalFitnessSum));
            }
            return newPopulation;
        }

        private TSPSpecimen RouletteSelectionMethodForOneSpecimen(double totalFitnessSum)
        {
            double randomNumber = random.NextDouble() * totalFitnessSum;
            double partialSum = 0;
            for (int i = 0; i < population.Count; i++)
            {
                partialSum += population[i].TotalTimeOfTravel(thief.currentVelocity);
                if (partialSum >= randomNumber)
                {
                    return population[i];
                }
            }

            return null;
        }

        #endregion

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
