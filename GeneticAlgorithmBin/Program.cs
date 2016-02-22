using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmBin
{
	class Program {
		private static void Main(string[] args) {
			const int EXECUTION_COUNT = 100;
			const int ITERATION_COUNT = 100;
			float cumulativeBestFitnessValues = 0;

			for (int j = 0; j < EXECUTION_COUNT; j++) {
				GeneticAlgorithm.parentMatingPool.Populate();

				for (int i = 0; i < ITERATION_COUNT; i++) {
					GeneticAlgorithm.Run();
				}

				List<int> fitnessValues = new List<int>(GeneticAlgorithm.PARENT_COUNT);
				foreach (var chromosome in GeneticAlgorithm.parentMatingPool._chromosomes) {
					fitnessValues.Add(chromosome.Fitness ?? 0);
					//fitnessValues.Add(chromosome.Fitness.HasValue ? chromosome.Fitness.Value : 0);
				}
				cumulativeBestFitnessValues += fitnessValues.Min();

				Console.WriteLine("R{0} | " + GeneticAlgorithm.parentMatingPool, j);
			}

			Console.WriteLine("Average is {0}", cumulativeBestFitnessValues/EXECUTION_COUNT);

			Console.ReadKey();
		}

		private static void Main2(string[] args)
		{
			const int EXECUTION_COUNT = 100;
			const int ITERATION_COUNT = 100;
			float cumulativeBestFitnessValues = 0;

			for (int k = 0; k < 64; k++) {
                GeneticAlgorithm.parentMatingPool = new MatingPool();
				
				for (int j = 0; j < EXECUTION_COUNT; j++)
				{
                    GeneticAlgorithm.parentMatingPool.Populate();

					for (int i = 0; i < ITERATION_COUNT; i++)
					{
                        GeneticAlgorithm.Run();
					}

					List<int> fitnessValues = new List<int>(GeneticAlgorithm.PARENT_COUNT);
					foreach (var chromosome in GeneticAlgorithm.parentMatingPool._chromosomes)
					{
						fitnessValues.Add(chromosome.Fitness ?? 0);
						//fitnessValues.Add(chromosome.Fitness.HasValue ? chromosome.Fitness.Value : 0);
					}
					cumulativeBestFitnessValues += fitnessValues.Min();

					//Console.WriteLine("R{0} | " + GeneticAlgorithm.parentMatingPool, j);
				}

				Console.WriteLine("Average is {0}", cumulativeBestFitnessValues / EXECUTION_COUNT);
				cumulativeBestFitnessValues = 0;
			}

			Console.ReadKey();
		}
	}
}
