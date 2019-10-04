using System;
using System.Collections.Generic;
using System.Text;

namespace genetic_algorithm
{
    public class DNA<T>
    {
        public T[] genes { get; private set; } //genes
        public float Fitness { get; private set; } //fitness function

        private Random random;
        private Func<T> getRandomGene;
        private Func<float,int> fitnessFunction;

        public DNA (int size, Random random, Func<T> getRandomGene, Func<float, int> fitnessFunction, bool shouldInitGenes = true)
        {
            genes = new T[size];
            this.random = random;
            this.genes = genes;
            this.fitnessFunction = fitnessFunction;

            if(shouldInitGenes)
            {
                for (int i = 0; i < genes.Length; i++)
                {
                    genes[i] = getRandomGene();
                }
            }

           
        }
        public float CalculateFitness(int index) //calculate fitness function 
        {
            Fitness = fitnessFunction(index);
            return Fitness;
        }
        public DNA<T> Crossingover (DNA<T> otherParent)    //crossover function
        {
            DNA<T> child = new DNA<T>(genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

            for (int i=0; i<genes.Length; i++)
            {
                child.genes[i] = random.NextDouble() < 0.5 ? genes[i] : otherParent.genes[i];
            }
            return child;
        }
        public void Mutate(float mutationRate)
        {
            for(int i=0; i<genes.Length; i++)
            {
                if(random.NextDouble() < mutationRate)
                {
                    genes[i] = getRandomGene();
                }
            }
        }
    }
}
