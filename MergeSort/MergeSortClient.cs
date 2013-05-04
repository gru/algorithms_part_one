using System;
using NUnit.Framework;

namespace MergeSort
{
	public class MergeSortClient
	{
		public static int cutoff = 7;

		public IComparable[] Sort(IComparable[] data) {
			Sort (data, new IComparable[data.Length], 0, data.Length - 1);
			return data;
		}

		private void Sort(IComparable[] data, IComparable[] aux, int lo, int hi)
		{
			if (hi <= lo + cutoff - 1) {
				data = InsertionSort (aux, lo, hi);
				return;
			}

			int mid = lo + (hi - lo) / 2;
			Sort (aux, data, lo, mid);
			Sort (aux, data, mid + 1, hi);

			if (!Less(data[mid + 1], data[mid])) { 
				// уже отсортирован. ! для случая когда data[mid + 1] == data[mid]
				return;
			}

			Merge (aux, data, lo, mid, hi);
		}

		private void Merge(IComparable[] data, IComparable[] aux, int lo, int mid, int hi) {

			Assert.IsTrue (IsSorted (data, lo, mid));
			Assert.IsTrue (IsSorted (data, mid + 1, hi));

			int i = lo;
			int j = mid + 1;
			for (int k = lo; k <= hi; k++) {
				if (i > mid) {
					aux [k] = data [j++];
				} else if (j > hi) {
					aux [k] = data [i++];
				} else if (Less (aux[j], aux[i])) {
					aux [k] = data [j++];
				} else {
					aux [k] = data [i++];
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

		public IComparable[] InsertionSort(IComparable[] data, int lo, int hi) {
			var n = data.Length;
			for (int i = lo; i < hi; i++) {
				for (int j = i; j > 0; j--) {
					if (Less (data[j], data[j - 1])) {
						Exchange(data, j, j - 1);
					}
				}
			}
			return data;
		}

		private void Exchange(IComparable[] data, int i, int j){
			var a = data [i];
			data [i] = data [j];
			data [j] = a;
		}
	}

	[TestFixture]
	public class MergeSortTests
	{
		[Test]
		public void Merge_sort_test() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c", "x" };
			var client = new MergeSortClient ();
			Assert.AreEqual (new [] { "a", "a", "b", "c", "c", "d", "x", "x" }, client.Sort (data));
		}
	}
}