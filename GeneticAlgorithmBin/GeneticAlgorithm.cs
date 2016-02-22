using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithmBin
{
	static class GeneticAlgorithm {
		public const int PARENT_COUNT = 6; // must be even
		public const int GENES_PER_CHROMOSOMES = 8;
		// private static int PopulationSize = 0;
		//static Tuple<int,String> MatingPool = new Tuple<int, string>(PopulationSize,String.Empty);
		//static Tuple<String,String>  Parents = new Tuple<string, string>(String.Empty, String.Empty);
		public static MatingPool parentMatingPool = new MatingPool();
		public static MatingPool childrenMatingPool;

		public static void Run() {
			//Debug.WriteLine("PM | " + parentMatingPool);
			// Phase 1
			childrenMatingPool = new MatingPool();

			// Phase 2
			for (int i = 0; i < PARENT_COUNT/2; i++) {
				childrenMatingPool.Add(CrossOver());
			}
			//Debug.WriteLine(childrenMatingPool.ToString());

			// Phase 3
			for (int i = 0; i < PARENT_COUNT; i++) {
				childrenMatingPool[i].Mutate(30);
			}
			//Debug.WriteLine("CH | " + childrenMatingPool.ToString());

			// Phase 4
			const int PARENT_COUNT_TO_BE_DELETED = PARENT_COUNT - 2;
			const int CHILDREN_COUNT_TO_BE_DELETED = PARENT_COUNT - 4;
			List<int> fitnessValues = new List<int>(PARENT_COUNT);

			for (int i = 0; i < PARENT_COUNT; i++) {
				fitnessValues.Add((int) parentMatingPool[i].Fitness);
			}
			int indexToBeDeleted = -1;
			for (int i = 0; i < PARENT_COUNT_TO_BE_DELETED; i++) {
				indexToBeDeleted = fitnessValues.IndexOf(fitnessValues.Max());

				fitnessValues[indexToBeDeleted] = -1;
				parentMatingPool._chromosomes[indexToBeDeleted].Clear();
			}

			fitnessValues.Clear();
			for (int i = 0; i < PARENT_COUNT; i++) {
				fitnessValues.Add((int) childrenMatingPool[i].Fitness);
			}
			indexToBeDeleted = -1;
			for (int i = 0; i < CHILDREN_COUNT_TO_BE_DELETED; i++) {
				indexToBeDeleted = fitnessValues.IndexOf(fitnessValues.Max());

				fitnessValues[indexToBeDeleted] = -1;
				childrenMatingPool._chromosomes[indexToBeDeleted].Clear();
			}

			// Combine survived parents and children
			int lastpos = 0;

			for (uint k = 0; k < PARENT_COUNT; k++) {
				if (parentMatingPool._chromosomes[k].Fitness != null) {
					continue;
				}

				while (childrenMatingPool._chromosomes[lastpos].Fitness == null) {
					lastpos++;
				}

				parentMatingPool._chromosomes[k] = new Chromosome(childrenMatingPool._chromosomes[lastpos++].genes);
			}
			//Debug.WriteLine("PA | " + parentMatingPool.ToString());
		}


		// TODO Move this from here
		public static Chromosome[] CrossOver() {
			uint mixstart = (uint) new Random((int) DateTime.Now.Ticks).Next(2, 7);
			//uint mixstart = 3;

			Chromosome[] parents = ParentSelection(ref parentMatingPool);
			Chromosome[] children = {
				new Chromosome(false), 
				new Chromosome(false) 
			};

			// Child 1
			Helpers.CopyArray(ref parents[0].genes, ref children[0].genes, 0, mixstart);
			Helpers.CopyArray(ref parents[1].genes, ref children[0].genes, mixstart);

			// Child 2
			Helpers.CopyArray(ref parents[1].genes, ref children[1].genes, 0, mixstart);
			Helpers.CopyArray(ref parents[0].genes, ref children[1].genes, mixstart);

			return children;
		}

		private static Chromosome[] ParentSelection(ref MatingPool matingPool) {
			int firstRandom;
			int secondRandom;

			Chromosome candidate1;
			Chromosome candidate2;
			Chromosome[] parents = new Chromosome[2];

			for (int i = 0; i < 2; i++) {
				firstRandom = Helpers.GetRandomInt(0, PARENT_COUNT);
				secondRandom = Helpers.GetRandomInt(0, PARENT_COUNT);

				// Make sure arbitrarily selected two choromosomes not the same ones...
				while (firstRandom == secondRandom) {
					secondRandom = Helpers.GetRandomInt(0, PARENT_COUNT);
				}

				// ...so candidate1 is not identical to candidate2.
				candidate1 = matingPool[firstRandom];
				candidate2 = matingPool[secondRandom];

				parents[i] = candidate1.Fitness <= candidate2.Fitness ? candidate1 : candidate2;
			}

			return parents;
		}


		public static void Representation() {}
	}
}
