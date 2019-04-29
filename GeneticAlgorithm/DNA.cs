using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class DNA
    {
        List<char> genes = new List<char>();
        public double fitness = 0;

        public string GetPhrase()
        {
            return String.Join("", genes.ToArray());
        }

        public void Fitness(string target)
        {
            int score = 0;

            char[] targetString = target.ToArray();

            for (int i = 0; i < this.genes.Count; i++)
            {
                if (this.genes[i] == targetString[i])
                {
                    score++;
                }
            }

            this.fitness = (double) score / (double) target.Length;
        }

        public DNA Crossover(DNA parent)
        {
            DNA child = new DNA(this.genes.Count);

            int midPoint = Program.rand.Next(0, this.genes.Count);

            for (int i = 0; i < this.genes.Count; i++)
            {
                if (i > midPoint)
                    child.genes[i] = this.genes[i];
                else
                    child.genes[i] = parent.genes[i];
            }

            return child;
        }

        public void Mutate(double rate)
        {
            for (int i = 0; i < this.genes.Count; i++)
            {
                double chance = Program.rand.NextDouble();

                if (chance < rate)
                    this.genes[i] = NewChar();
            }
        }

        private char NewChar()
        {
            //Console.WriteLine(chars[num]);
            return (char) Program.rand.Next(32, 128);
        }


        public DNA(int popMax)
        {
            for (int i = 0; i < popMax; i++)
            {
                this.genes.Add(NewChar());
            }
        }
    }
}
