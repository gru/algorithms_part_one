using System;
using NUnit.Framework;

namespace QuickSort
{
	public class QuickSortClient
	{
		public IComparable[] Sort(IComparable[] data) {
			data = Shuffle (data, 1);
			Sort (data, 0, data.Length - 1);
			return data;
		}

		private void Sort(IComparable[] data, int lo, int hi) {
			if (hi <= lo) {
				return;
			}
			var j = Partition (data, lo, hi);
			Sort (data, lo, j - 1);
			Sort (data, j + 1, hi);
		}

		public int Partition(IComparable[] data, int lo, int hi) {
			int i = lo;
			int j = hi + 1;
			while (true) {
				while (Less(data[++i], data[lo])) {
					if (i == hi) {
						break;
					}
				}
				while (Less(data[lo], data[--j])) {
					if (j == lo) {
						break;
					}
				}
				if (i >= j) {
					break;
				}
				Exchange (data, i, j);
			}
			Exchange (data, lo, j);
			return j;
		}

		public IComparable[] Shuffle(IComparable[] data, int seed) {
			var n = data.Length;
			var random = new Random (seed);
			for (int i = 0; i < n; i++) {
				var r = random.Next (0, i + 1);
				Exchange (data, i, r);
			}
			return data;
		}

		private bool Less(IComparable a, IComparable b){
			return a.CompareTo (b) < 0;
		}

		private void Exchange(IComparable[] data, int i, int j){
			var a = data [i];
			data [i] = data [j];
			data [j] = a;
		}
	}

	[TestFixture]
	public class QuickSortTests
	{
		[Test]
		public void Quick_sort_tests() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c", "x" };
			var client = new QuickSortClient ();
			Assert.AreEqual (new [] { "a", "a", "b", "c", "c", "d", "x", "x" }, client.Sort (data));
		}
	}
}

