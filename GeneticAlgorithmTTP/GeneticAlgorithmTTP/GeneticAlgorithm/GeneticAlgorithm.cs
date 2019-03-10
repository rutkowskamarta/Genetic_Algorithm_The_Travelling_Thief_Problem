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
        private DataLoaded dataLoaded = DataLoaded.GetInstance();

        private int currentGeneration = 0;
        private int stagnationCounter = 0;

        private List<TSPSpecimen> oldPopulation;
        private List<TSPSpecimen> newPopulation;

        private TSPSpecimen bestSolution;

        private Random random;

        private delegate TSPSpecimen mutationDelegate(TSPSpecimen specimen);
        private mutationDelegate mutationFunction;

        private delegate List<TSPSpecimen> selectionDelegate();
        private selectionDelegate selectionFunction;

        private SELECTION_METHOD selectionMethod = SELECTION_METHOD.TURNIEJ;
        private MUTATION_METHOD mutationMethod = MUTATION_METHOD.INVERSE;

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
                    case SELECTION_METHOD.RANKING:
                        selectionFunction = RankSelectionMethod;
                        break;
                    case SELECTION_METHOD.TURNIEJ:
                        selectionFunction = TournamentSelectionMethod;
                        break;
                }
            }
        }


        public GeneticAlgorithm()
        {
            random = new Random();

            MutationMethod = mutationMethod;
            SelectionMethod = selectionMethod;

            InitializePopulation();
        }

        private void InitializePopulation()
        {
            oldPopulation = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
                oldPopulation.Add(new TSPSpecimen());
        }

        public TSPSpecimen GeneticCycle()
        {
            bestSolution = FindBest(oldPopulation).Clone();
            SaveSolutionToFile(bestSolution, FILE_ANNOTATION_FIRST);

            Evaluate(oldPopulation);

            WriteLine("BEST: " + bestSolution.objectiveFunction + " " + bestSolution.CitiesToString());

            while (currentGeneration < NUMBER_OF_GENERATIONS && stagnationCounter<STAGNATION_FACTOR*NUMBER_OF_GENERATIONS)
            {
                currentGeneration++;

                newPopulation = Selection();
                Crossover(newPopulation);
                Mutation(newPopulation);
                Evaluate(newPopulation);

                TSPSpecimen bestSolutionInNewPopolation = FindBest(newPopulation);
                //WriteLine("BEST: " + bestSolutionInNewPopolation.objectiveFunction);

                if (bestSolutionInNewPopolation.objectiveFunction > bestSolution.objectiveFunction)
                {
                    bestSolution = bestSolutionInNewPopolation.Clone();

                    stagnationCounter = 0;
                }
                else
                {
                    stagnationCounter++;
                }

                oldPopulation = newPopulation;
                //WriteLine("BEST: " + bestSolution.objectiveFunction + " " + bestSolution.CitiesToString());

            }

            return bestSolution;
        }

        private TSPSpecimen FindBest(List<TSPSpecimen> examinedPopulation)
        {
            return examinedPopulation.OrderByDescending(i1 => i1.objectiveFunction).First();
        }

        private List<TSPSpecimen> Selection()
        {
            return selectionFunction();
        }

        private void Mutation(List<TSPSpecimen> population)
        {
            population.ForEach(p => Mutate(p));
        }

        private void Crossover(List<TSPSpecimen> population)
        {
            for (int i = 0; i < POPULATION_SIZE; i += 2)
            {
                if (i + 1 < POPULATION_SIZE)
                {
                    TSPSpecimen[] children = Cross(population[i], population[i + 1]);
                    population[i] = children[0];
                    population[i + 1] = children[1];
                }
            }
        }

        private void Evaluate(List<TSPSpecimen> population)
        {
            for (int i = 0; i < population.Count; i++)
                population[i].SetObjectiveFunction();
        }

        #region SELECTION_METHODS

        private List<TSPSpecimen> TournamentSelectionMethod()
        {
            
            List<TSPSpecimen> nextPopulation = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                nextPopulation.Add(TournamentSelectionMethodForOneSpecimen());
            }
            return nextPopulation;
        }

        private TSPSpecimen TournamentSelectionMethodForOneSpecimen()
        {
            List<TSPSpecimen> chosen = new List<TSPSpecimen>();
            List<int> randomIndexes = new List<int>();
            while (randomIndexes.Count < TOURNAMENT_SIZE)
            {
                int randomIndex = random.Next(0, POPULATION_SIZE);
                while (randomIndexes.Contains(randomIndex))
                {
                    randomIndex = random.Next(0, POPULATION_SIZE);
                }
                randomIndexes.Add(randomIndex);
            }
            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                chosen.Add(oldPopulation[randomIndexes[i]]);
            }

            chosen = chosen.OrderByDescending(p=>p.objectiveFunction).ToList();
            return chosen.First();
        }

        private List<TSPSpecimen> RankSelectionMethod()
        {
            List<TSPSpecimen> populationRank = oldPopulation.OrderBy(p => p.objectiveFunction).ToList<TSPSpecimen>();
            List<TSPSpecimen> nextPopulation = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                nextPopulation.Add(RankSelectionMethodForOneSpecimen(populationRank));
            }
            return nextPopulation;
        }

        private TSPSpecimen RankSelectionMethodForOneSpecimen(List<TSPSpecimen> populationRank)
        {
            int randomNumber = random.Next(0, POPULATION_SIZE);
            return populationRank[randomNumber];

        }

        private List<TSPSpecimen> RouletteSelectionMethod()
        {
            int minimumObjectiveFunction = oldPopulation.Min(specimen => specimen.objectiveFunction);
            if (minimumObjectiveFunction >= 0)
            {
                minimumObjectiveFunction = 0;
            }

            int totalFitnessSum = oldPopulation.Sum(specimen => specimen.objectiveFunction-minimumObjectiveFunction);

            List<TSPSpecimen> nextPopulation = new List<TSPSpecimen>(POPULATION_SIZE);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                nextPopulation.Add(RouletteSelectionMethodForOneSpecimen(totalFitnessSum, minimumObjectiveFunction));
            }
            return nextPopulation;
        }

        private TSPSpecimen RouletteSelectionMethodForOneSpecimen(double totalFitnessSum, double minimumObjectiveFunction)
        {
            double randomNumber = random.NextDouble() * totalFitnessSum;
            double partialSum = 0;
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                partialSum += (oldPopulation[i].objectiveFunction)-minimumObjectiveFunction;
                if (partialSum >= randomNumber)
                {
                    return oldPopulation[i];
                }
            }
            return null;
        }

        #endregion

        #region MUTATION_FUNCTIONS

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

        private TSPSpecimen SwapMutation(TSPSpecimen specimen)
        {
            int index1 = random.Next(0, dataLoaded.totalNumberOfCities);
            int index2 = random.Next(0, dataLoaded.totalNumberOfCities);
            while (index2 == index1)
            {
                index2 = random.Next(0, dataLoaded.totalNumberOfCities);
            }
            CityElement city1 = specimen.citiesVisitedInOrder[index1];
            CityElement city2 = specimen.citiesVisitedInOrder[index2];

            specimen.citiesVisitedInOrder[index1] = city2;
            specimen.citiesVisitedInOrder[index2] = city1;
            return specimen;
        }

        private TSPSpecimen InverseMutation(TSPSpecimen specimen)
        {
            int index1 = random.Next(0, dataLoaded.totalNumberOfCities);
            int index2 = random.Next(0, dataLoaded.totalNumberOfCities);
            while (index2 == index1)
            {
                index2 = random.Next(0, dataLoaded.totalNumberOfCities);
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
                specimen.citiesVisitedInOrder[i] = citiesInRange[i-index1];
            }

            return specimen;
        }

        #endregion

        #region CROSS_FUNCTIONS

        private TSPSpecimen[] Cross(TSPSpecimen parent1, TSPSpecimen parent2)
        {
            double chance = random.NextDouble();
            if (chance <= PROBABILITY_OF_CROSSOVER)
                return OXCross(parent1, parent2);
            else
                return new TSPSpecimen[] { parent1, parent2 };
        }

        private TSPSpecimen[] OXCross(TSPSpecimen parent1, TSPSpecimen parent2)
        {
            int crossPoint1 = random.Next(0, dataLoaded.totalNumberOfCities);
            int crossPoint2 = random.Next(0, dataLoaded.totalNumberOfCities);
            while (crossPoint2 == crossPoint1)
            {
                crossPoint2 = random.Next(0, dataLoaded.totalNumberOfCities);
            }

            if (crossPoint2 < crossPoint1)
            {
                int temp = crossPoint2;
                crossPoint2 = crossPoint1;
                crossPoint1 = temp;
            }
            TSPSpecimen child1 = OXCrossBasedOnFirstParent(parent1, parent2, crossPoint1, crossPoint2);
            TSPSpecimen child2 = OXCrossBasedOnFirstParent(parent2, parent1, crossPoint1, crossPoint2);

            return new TSPSpecimen[] { child1, child2 };
        }

        private TSPSpecimen OXCrossBasedOnFirstParent(TSPSpecimen parent1, TSPSpecimen parent2, int crossPoint1, int crossPoint2)
        {
            TSPSpecimen child = new TSPSpecimen();

            List<int> crossSection = new List<int>();

            for (int i = crossPoint1; i < crossPoint2; i++)
            {
                crossSection.Add(parent1.citiesVisitedInOrder[i].index);
            }

            List<int> restOfGenes = new List<int>();

            for (int i = crossPoint2; i < dataLoaded.totalNumberOfCities; i++)
            {
                if (!crossSection.Contains(parent2.citiesVisitedInOrder[i].index))
                    restOfGenes.Add(parent2.citiesVisitedInOrder[i].index);
            }

            for (int i = 0; i < crossPoint2; i++)
            {
                if (!crossSection.Contains(parent2.citiesVisitedInOrder[i].index))
                    restOfGenes.Add(parent2.citiesVisitedInOrder[i].index);
            }

            List<int> childCities = new List<int>();

            int secondPartOfGenes = restOfGenes.Count - crossPoint1;

            childCities.AddRange(restOfGenes.Skip(secondPartOfGenes));
            childCities.AddRange(crossSection);
            childCities.AddRange(restOfGenes.Take(secondPartOfGenes));
            child.citiesVisitedInOrder.Clear();

            for (int i = 0; i < dataLoaded.totalNumberOfCities; i++)
            {
                child.citiesVisitedInOrder.Add(dataLoaded.cities[childCities[i] - 1].Clone());
            }
            return child;
        }
        #endregion
    }
}
