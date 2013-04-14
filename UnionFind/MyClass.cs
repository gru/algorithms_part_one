using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

namespace UnionFind
{
	public class UF
	{
		private int[] items;

		public UF (int n) {
			items = new int[n];
		}

		public void union(int p1, int p2) {
			var comp1 = items [p1];
			var comp2 = items [p2];
			for (int i = 0; i < items.Length; i++) {
				if (items[i] == comp1)
					items[i] = comp2;
			}
		}

		public bool connected(int p, int q){
			return items [p] == items [q];
		}
	}

	[TestFixture]
	public class UFTests{
		private UF uf;

		[SetUp]
		public void Setup(){
			var connections = new ConnectionCollection (){
				{ 4, 3 },
				{ 3, 8 },
				{ 6, 5 },
				{ 9, 4 },
				{ 2, 1 },
				{ 8, 9 },
				{ 5, 0 },
				{ 7, 2 },
				{ 6, 1 },
				{ 1, 0 },
				{ 6, 7 },
			};

			uf = new UF (connections.Count);
			foreach (var e in connections) {
				uf.union((int) e.P1,(int) e.P2);
			}

			for (int i = 0; i < connections.Count; i++) {
				for (int j = i; j < connections.Count; j++) {
					if (uf.connected(i, j))
						Console.WriteLine ("{0} {1}", i, j);
				}
			}
		}

		[Test]
		public void Find_test() {
			Assert.IsFalse (uf.connected (1, 6));
			Assert.IsTrue (uf.connected (8, 3));
		}

		[Test]
		public void Union_test() {
			Assert.IsFalse (uf.connected (1, 6));
			uf.union (1, 6);
			Assert.IsTrue (uf.connected (1, 6));
		}
	}

	class ConnectionCollection : IEnumerable<ConnectionCollection>
	{
		private List<ConnectionCollection> values = 
			new List<ConnectionCollection>();

		public ConnectionCollection ()
		{
		}

		public ConnectionCollection (int p1, int p2)
		{
			P1 = p1;
			P2 = p2;
		}

		public void Add(int p1, int p2)
		{
			values.Add (new ConnectionCollection (p1, p2));
		}

		public int P1 {
			get;
			set;
		}

		public int P2 {
			get;
			set;
		}

		public int Count {
			get {
				return values.Count;
			}
		}

		#region IEnumerable implementation

		public IEnumerator<ConnectionCollection> GetEnumerator ()
		{
			return values.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
	}
}

