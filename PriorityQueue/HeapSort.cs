using System;
using NUnit.Framework;

namespace PriorityQueue
{
	public class HeapSort
	{
		public void Sort(IComparable[] data)
		{
			int n = data.Length;
			for (int k = n / 2; k >= 1; k--) {
				Sink (data, k, n);
			}
			while (n > 1) {
				Exchange(data, 1, n);
				Sink(data, 1, --n);
			}
		}

		private void Sink(IComparable[] pq, int k, int n)
		{
			while (2 * k <= n) {
				int j = 2 * k;
				if (j < n && Less (pq, j, j + 1)) {
					j++;
				}
				if (!Less (pq, k, j)) {
					break;
				}
				Exchange (pq, k, j);
				k = j;
			}
		}

		private void Exchange(IComparable[] pq, int i, int j)
		{
			i = i - 1; j = j - 1;
			var a = pq [i];
			pq [i] = pq [j];
			pq [j] = a;
		}

		private bool Less(IComparable[] pq,int i, int j) 
		{
			i = i - 1; j = j - 1;
			return pq [i].CompareTo (pq [j]) < 0;
		}
	}

	[TestFixture]
	public class HeapSortTests
	{
		[Test]
		public void Heap_sort_test() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c", "x" };
			var client = new HeapSort ();
			client.Sort (data);
			Assert.AreEqual (new [] { "a", "a", "b", "c", "c", "d", "x", "x" }, data);
		}
	}
}

