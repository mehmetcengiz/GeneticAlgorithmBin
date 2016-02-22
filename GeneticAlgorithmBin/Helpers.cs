using System;
using System.Collections.Generic;

namespace GeneticAlgorithmBin
{
	static class Helpers {
		private static Random _random = new Random((int) DateTime.Now.Ticks);

		public static void Shuffle<T>(T[] array) {
			int n = array.Length;

			while (n > 1) {
				int k = _random.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}

		public static int GetRandomInt(int minValue = int.MinValue, int maxValue = int.MaxValue) {
			return _random.Next(minValue, maxValue);
		}

		public static bool GetBoolByChance(int chance, int outOf = 100) {
			return _random.Next(outOf) < chance;
		}

		/*public static BitArray GetFitnessValue() {
            BitArray fitnessValue = new BitArray(20);
            return fitnessValue;
        }*/

		public static int GetIntFromSignedBitArray(List<int> array) {
			int result = 0;
			int arrayLength = array.Count;
			bool isNegative = array[0] == 1;

			for (int i = 1; i < arrayLength; i++) {
				result += array[i]*(int) Math.Pow(2d, arrayLength - (i + 1));
			}

			return (int) (isNegative ? Math.Pow(2d, arrayLength - 1) * -1 + result : result);
		}

		public static void RemoveFromArray<T>(ref T[] array, T item) {
			int n = array.Length;

			while (n-- > 0) {
				if (Equals(array[n], item)) {
					array[n] = default(T);

					n = 0;
				}
			}
		}

		public static void CopyArray<T>(ref T[] source, ref T[] destination, uint start = 0, uint? length = null) {
			uint sourceLength = (uint) source.Length;
			uint destinationLength = (uint) destination.Length;

			if (!Equals(sourceLength, destinationLength)) {
				throw new IndexOutOfRangeException("Source and destination array's sizes mismatch.");
			}

			if (!length.HasValue) {
				length = sourceLength;
			}

			for (uint i = start; i < length; i++) {
				destination[i] = source[i];
			}
		}
	}
}
