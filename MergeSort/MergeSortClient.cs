using System;
using NUnit.Framework;

namespace MergeSort
{
	public class MergeSortClient
	{
		public IComparable[] Sort(IComparable[] data) {
			Sort (data, new IComparable[data.Length], 0, data.Length - 1);
			return data;
		}

		private void Sort(IComparable[] data, IComparable[] aux, int lo, int hi)
		{
			if (lo >= hi) {
				return;
			}

			int mid = lo + (hi - lo) / 2;
			Sort (data, aux, lo, mid);
			Sort (data, aux, mid + 1, hi);
			Merge (data, aux, lo, mid, hi);
		}

		private void Merge(IComparable[] data, IComparable[] aux, int lo, int mid, int hi) {

			Assert.IsTrue (IsSorted (data, lo, mid));
			Assert.IsTrue (IsSorted (data, mid + 1, hi));

			for (int k = lo; k <= hi; k++) {
				aux [k] = data [k];
			}

			int i = lo;
			int j = mid + 1;
			for (int k = lo; k <= hi; k++) {
				if (i > mid) {
					data [k] = aux [j++];
				} else if (j > hi) {
					data [k] = aux [i++];
				} else if (Less (aux[j], aux[i])) {
					data [k] = aux [j++];
				} else {
					data [k] = aux [i++];
				}
			}

			Assert.IsTrue (IsSorted (data, lo, hi));
		}

		private bool Less(IComparable a, IComparable b){
			return a.CompareTo (b) < 0;
		}

		public bool IsSorted(IComparable[] data, int lo, int hi) {
			for (int i = lo + 1; i <= hi; i++) {
				if (Less (data [i], data [i - 1])) {
					return false;
				}
			}
			return true;
		}
	}

	[TestFixture]
	public class MergeSortTests
	{
		[Test]
		public void Merge_sort_test() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c" };
			var client = new MergeSortClient ();
			Assert.AreEqual (new [] { "a", "a", "b", "c", "c", "d", "x" }, client.Sort (data));
		}
	}
}