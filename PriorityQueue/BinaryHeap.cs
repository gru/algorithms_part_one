using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace PriorityQueue
{
	public class BinaryHeap<TKey> 
		where TKey : class, IComparable
	{
		private TKey[] pq;
		private int n = 0;

		public BinaryHeap (int capacity)
		{
			pq = new TKey[capacity + 1];
		}

		public void Insert(TKey key)
		{
			pq [++n] = key;
			Swim (n);
		}

		public TKey DeleteMax() {
			var key = pq [1];
			Exchange (1, n--);
			Sink (1);
			pq [n + 1] = null;
			return key;
		}

		private void Swim(int k)
		{
			while (k > 1 && Less(k / 2, k)) {
				Exchange (k, k / 2);
				k = k / 2;
			}
		}

		private void Sink(int k)
		{
			while (2 * k <= n) {
				int j = 2 * k;
				if (j < n && Less (j, j + 1)) {
					j++;
				}
				if (!Less (k, j)) {
					break;
				}
				Exchange (k, j);
				k = j;
			}
		}

		private void Exchange(int i, int j)
		{
			var a = pq [i];
			pq [i] = pq [j];
			pq [j] = a;
		}

		private bool Less(int i, int j) 
		{
			return pq [i].CompareTo (pq [j]) < 0;
		}
	}

	[TestFixture]
	public class HeapTests
	{
		[Test]
		public void Heap_tests() {
			var data = new[] { "1", "2", "4", "6", "1", "1", "1", "2", "3", "4" };
			var heap = new BinaryHeap<string> (data.Count());
			for (int i = 0; i < data.Length; i++) {
				heap.Insert (data [i]);
			}
			Assert.AreEqual ("6", heap.DeleteMax ());
			Assert.AreEqual ("4", heap.DeleteMax ());
		}
	}
}