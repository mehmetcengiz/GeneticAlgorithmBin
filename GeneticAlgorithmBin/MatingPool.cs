using System;

namespace GeneticAlgorithmBin {
	class MatingPool {
		public Chromosome[] _chromosomes = new Chromosome[GeneticAlgorithm.PARENT_COUNT];
		private int _chromosomesCount = 0;

		public MatingPool() {
			for (int i = 0; i < _chromosomes.Length; i++) {
				// Do NOT auto populate
				_chromosomes[i] = new Chromosome(false);
			}
		}

		public void Populate(int chromosomesCount = GeneticAlgorithm.PARENT_COUNT) {
			for (int i = 0; i < chromosomesCount; i++) {
				_chromosomes[i] = new Chromosome();
			}
		}

		public Chromosome this[int index] {
			get { return _chromosomes[index]; }
			set { _chromosomes[index] = value; }
		}

		public void Add(Chromosome[] chromosomes) {
			for (int i = 0; i < chromosomes.Length; i++) {
				Helpers.CopyArray(ref chromosomes[i].genes, ref _chromosomes[_chromosomesCount + i].genes);
			}

			_chromosomesCount += chromosomes.Length;
		}

		public override string ToString() {
			string fitnesses = "";

			foreach (var chromosome in _chromosomes) {
				fitnesses += (chromosome.Fitness.HasValue ? chromosome.Fitness.Value.ToString() : "null") + " | ";
			}
			fitnesses = fitnesses.TrimEnd(' ', '|');

			foreach (var chromosome in _chromosomes) {
				fitnesses += Environment.NewLine + chromosome;
			}

			return fitnesses.TrimEnd(' ', '|');
		}
	}
}
