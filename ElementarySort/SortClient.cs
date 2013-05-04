using System;
using NUnit.Framework;

namespace ElementarySort
{
	public class SortClient
	{
		public IComparable[] SelectionSort(IComparable[] data) {
			var n = data.Length;
			for (int i = 0; i < n; i++) {
				int min = i;
				for (int j = i + 1; j < n; j++) {
					if (Less (data [j], data [min])) {
						min = j;
					}
				}
				Exchange (data, min, i);
			}
			return data;
		}

		public IComparable[] InsertionSort(IComparable[] data) {
			var n = data.Length;
			for (int i = 0; i < n; i++) {
				for (int j = i; j > 0; j--) {
					if (Less (data[j], data[j - 1])) {
					    Exchange(data, j, j - 1);
					}
				}
			}
			return data;
		}

		public bool IsSorted(IComparable[] data) {
			for (int i = 1; i < data.Length; i++) {
				if (Less (data [i], data [i - 1])) {
					return false;
				}
			}
			return true;
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
	public class SortClientTests
	{
		[Test]
		public void Is_sorted_test(){
			var client = new SortClient ();
			var sortedData = new[] { "a", "b", "c", "d" };
			Assert.IsTrue (client.IsSorted (sortedData));

			var data = new[] { "b", "c", "d", "a" };
			Assert.IsFalse (client.IsSorted (data));
		}

		[Test]
		public void Selection_sort_test(){
			var data = new [] { "1", "2", "3", "0" };
			var client = new SortClient ();
			Assert.AreEqual (new [] { "0", "1", "2", "3" }, client.SelectionSort (data));
		}

		[Test]
		public void Insertion_sort_test(){
			var data = new [] { "1", "2", "3", "0" };
			var client = new SortClient ();
			Assert.AreEqual (new [] { "0", "1", "2", "3" }, client.InsertionSort (data));
		}
	}
}

