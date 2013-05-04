using System;
using NUnit.Framework;

namespace ElementarySort
{
	public static class SortClient
	{
		public static IComparable[] SelectionSort(IComparable[] data) {
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

		public static IComparable[] InsertionSort(IComparable[] data) {
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

		public static IComparable[] ShellSort(IComparable[] data) {
			var n = data.Length;

			var h = 1;
			while (h < n / 3) {
				h = 3 * h + 1;
			}

			while(h >= 1) {
				for (int i = h; i < n; i++) {
					for (int j = i; j >= h; j -= h) {
						if (Less(data[j], data[j - h])) {
							Exchange (data, j, j - h);
						}
					}
				}

				h = h / 3;
			}

			return data;
		}

		public static IComparable[] Shuffle(IComparable[] data, int seed) {
			var n = data.Length;
			var random = new Random (seed);
			for (int i = 0; i < n; i++) {
				var r = random.Next (0, i + 1);
				Exchange (data, i, r);
			}
			return data;
		}

		public static bool IsSorted(IComparable[] data) {
			for (int i = 1; i < data.Length; i++) {
				if (Less (data [i], data [i - 1])) {
					return false;
				}
			}
			return true;
		}

		private static bool Less(IComparable a, IComparable b){
			return a.CompareTo (b) < 0;
		}

		private static void Exchange(IComparable[] data, int i, int j){
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
			var sortedData = new[] { "a", "b", "c", "d" };
			Assert.IsTrue (SortClient.IsSorted (sortedData));

			var data = new[] { "b", "c", "d", "a" };
			Assert.IsFalse (SortClient.IsSorted (data));
		}

		[Test]
		public void Selection_sort_test(){
			var data = new [] { "1", "2", "3", "0" };
			Assert.AreEqual (new [] { "0", "1", "2", "3" }, SortClient.SelectionSort (data));
		}

		[Test]
		public void Insertion_sort_test(){
			var data = new [] { "1", "2", "3", "0" };
			Assert.AreEqual (new [] { "0", "1", "2", "3" }, SortClient.InsertionSort (data));
		}
		
		[Test]
		public void Shell_sort_test(){
			var data = new [] { "1", "2", "3", "0" };
			Assert.AreEqual (new [] { "0", "1", "2", "3" }, SortClient.ShellSort (data));
		}

		[Test]
		public void Shuffle_test(){
			var data = new [] { "1", "2", "3", "0" };
			Assert.AreNotEqual (data.Clone(), SortClient.Shuffle (data, 3));
		}
	}
}