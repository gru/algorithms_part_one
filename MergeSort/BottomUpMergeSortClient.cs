using System;
using NUnit.Framework;

namespace MergeSort
{
	public class BottomUpMergeSortClient
	{
		private static IComparable[] aux;

		public IComparable[] Sort(IComparable[] data) {
			aux = new IComparable[data.Length];
			int n = data.Length;
			for (int sz = 1; sz < n; sz += sz) {
				for (int lo = 0; lo < n - sz; lo += sz + sz) {
					Merge (data, lo, lo + sz - 1, Math.Min (lo + sz + sz - 1, n - 1));
				}
			}
			return data;
		}

		private void Merge(IComparable[] data, int lo, int mid, int hi) {

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

		public bool IsSorted(IComparable[] data, int lo, int hi) {
			for (int i = lo + 1; i <= hi; i++) {
				if (Less (data [i], data [i - 1])) {
					return false;
				}
			}
			return true;
		}

		private bool Less(IComparable a, IComparable b){
			return a.CompareTo (b) < 0;
		}
	}

	[TestFixture]
	public class BottomUpMergeSortTests
	{
		[Test]
		public void Bottom_up_merge_sort_test() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c", "x" };
			var client = new BottomUpMergeSortClient ();
			Assert.AreEqual (new [] { "a", "a", "b", "c", "c", "d", "x", "x" }, client.Sort (data));
		}
	}
}

