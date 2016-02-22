using System;
using System.Linq;

namespace GeneticAlgorithmBin
{
	class Chromosome {
		private int? _fitness = null;

		public int[] genes = { 0, 1, 0, 1, 0, 1, 0, 1 };

		public Chromosome(bool autoPopulate = true) {
			if (autoPopulate) {
				Helpers.Shuffle(genes);
			}
		}

		public Chromosome(int[] genes) {
			this.genes = genes;
		}

		public int? Fitness {
			get {
				if (_fitness == null && genes[0] > -1) {
					_fitness = CalculateFitness();
				}

				return _fitness;
			}
		}

		private int CalculateFitness() {
			int x = Helpers.GetIntFromSignedBitArray(genes.ToList());
			return (int) Math.Pow(x, 2d) + (16 * x);
		}

		public void Mutate(int chance, int outOf = 100) {
			bool[] mutationChances = new bool[10];
			for (int i = 0; i < GeneticAlgorithm.GENES_PER_CHROMOSOMES; i++) {
				mutationChances[i] = Helpers.GetBoolByChance(chance, outOf);
			}

			for (int j = 0; j < GeneticAlgorithm.GENES_PER_CHROMOSOMES; j++) {
				if (!mutationChances[j]) {
					continue;
				}

				genes[j] = (genes[j] + 1)%2;
			}

			// Invalidate fitness
			_fitness = null;
		}

		public void Clear() {
			for (int i = 0; i < GeneticAlgorithm.GENES_PER_CHROMOSOMES; i++) {
				genes[i] = -1;
			}

			_fitness = null;
		}

		public override string ToString() {
			string result = "";

			foreach (var gene in genes) {
				result += gene;
			}

			return result;
		}
	}
}
