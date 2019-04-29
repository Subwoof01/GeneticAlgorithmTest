using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class Population
    {
        private string _target;
        private double _mutationRate;
        private int _popMax;
        private int _perfectScore = 1;
        private bool _isFinished = false;

        public int generations = 0;


        public List<DNA> matingPool;
        public List<DNA> population;

        public void CalcFitness()
        {
            for (int i = 0; i < population.Count; i++)
            {
                population[i].Fitness(_target);
            }
        }

        public void NaturalSelection()
        {
            matingPool.Clear();

            //double maxFitness = 0;
            //for (int i = 0; i < this.population.Count; i++)
            //{
            //    if (this.population[i].fitness > maxFitness)
            //    {
            //        maxFitness = this.population[i].fitness;
            //    }
            //}

            //double ratio = 100 / maxFitness;
            //List<double> normalisedFitness = this.population.Select(i => i.fitness * ratio).ToList();

            

            for (int i = 0; i < this.population.Count; i++)
            {
                int n = (int) (this.population[i].fitness * 100);
                for (int j = 0; j < n; j++)
                {
                    this.matingPool.Add(this.population[i]);
                }
            }
        }

        public void Generate()
        {
            for (int i = 0; i < this.population.Count; i++)
            {
                Random rand = new Random();

                int a = rand.Next(0, this.matingPool.Count);
                int b = rand.Next(0, this.matingPool.Count);

                DNA parentA = this.matingPool.ElementAt(a);
                DNA parentB = this.matingPool.ElementAt(b);

                DNA child = parentA.Crossover(parentB);
                child.Mutate(this._mutationRate);
                this.population[i] = child;
            }

            this.generations++;
        }

        public string GetBest()
        {
            double top = 0.0;
            int index = 0;
            for (int i = 0; i < this.population.Count; i++)
            {
                if (this.population[i].fitness > top)
                {
                    index = i;
                    top = this.population[i].fitness;
                }
            }

            if (top == _perfectScore)
                _isFinished = true;
            return this.population[index].GetPhrase();
        }

        public bool IsFinished()
        {
            return _isFinished;
        }

        public double AverageFitness()
        {
            double total = 0;
            for (int i = 0; i < this.population.Count; i++)
            {
                total += this.population[i].fitness;
            }
            return total / this.population.Count;
        }

        public Population(string target, double mutationRate, int popMax)
        {
            _target = target;
            _mutationRate = mutationRate;
            _popMax = popMax;

            matingPool = new List<DNA>();
            population = new List<DNA>();

            for (int i = 0; i < popMax; i++)
            {
                this.population.Add(new DNA(target.Length));
            }

            CalcFitness();
        }
    }
}
