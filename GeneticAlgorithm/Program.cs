using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        public static Random rand = new Random();

        Population population;

        string target;
        int maxPopulation;
        double mutationRate;

        void Setup()
        {
            target = "To be or not to be.";
            maxPopulation = 200;
            mutationRate = 0.01;

            population = new Population(target, mutationRate, maxPopulation);
        }

        void MainLoop()
        {
            while (!population.IsFinished())
            {
                population.NaturalSelection();
                population.Generate();
                population.CalcFitness();
                string result = population.GetBest();
                Console.WriteLine("Current best: " + result);
                Console.WriteLine("Average fitness: " + (int)(population.AverageFitness() * 100) + "%");
                Console.WriteLine("Generations: " + population.generations);
                Console.WriteLine("Mutation rate: " + mutationRate * 100 + "%");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Setup();
            program.MainLoop();
        }
    }
}
