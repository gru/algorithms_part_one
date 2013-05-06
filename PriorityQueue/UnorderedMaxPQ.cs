using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

namespace PriorityQueue
{
	public class UnorderedMaxPQ<TKey> : IEnumerable<TKey>
		where TKey : IComparable
	{
		private TKey[] pq;
		private int n = 0;

		public UnorderedMaxPQ (int size)
		{
			pq = new TKey[size];
		}

		public void Insert(TKey key) 
		{
			pq [n++] = key;
		}

		public TKey DeleteMax()
		{
			int max = 0;
			for (int i = 0; i < n; i++) {
				if (Less(max, i)) {
					max = i;
				}
			}
			Exchange (max, n - 1);
			return pq[--n];
		}

		public int Count {
			get {
				return n;
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

		#region IEnumerable implementation

		public IEnumerator<TKey> GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return pq.GetEnumerator ();
		}

		#endregion
	}

	[TestFixture]
	class MinPQTests
	{
		[Test]
		public void UnorderedMaxPQ_tests() {
			var data = new[] { 1, 2, 4, 6, 1, 1, 1, 2, 3, 4 };
			var m = 3;
			var pq = new UnorderedMaxPQ<int> (m);
			foreach (var item in data) {
				pq.Insert (item);
				if (pq.Count >= m)
					pq.DeleteMax ();
			}
			Assert.AreEqual (new[] { 1, 1, 4 }, pq); 
		}
	}
}

