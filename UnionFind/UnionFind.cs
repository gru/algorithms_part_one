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
			for (int i = 0; i < items.Length; i++) {
				items[i] = i;
			}
		}

		public void union(int p1, int p2) {
			var comp1 = items [p1];
			var comp2 = items [p2];
			for (int i = 0; i < items.Length; i++) {
				if (items[i] == comp1)
					items[i] = comp2;
			}
		}

		public bool connected(int p1, int p2){
			return items [p1] == items [p2];
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
			};

			uf = new UF (10);
			foreach (var e in connections) {
				uf.union((int) e.P1,(int) e.P2);
			}

		}

		[Test]
		public void Find_test() {
			Assert.IsFalse (uf.connected (1, 3));
			Assert.IsTrue (uf.connected (8, 4));
		}

		[Test]
		public void Union_test() {
			Assert.IsFalse (uf.connected (1, 6));
			uf.union (1, 6);
			Assert.IsTrue (uf.connected (1, 6));
		}
	}

	public class ConnectionCollection : IEnumerable<ConnectionCollection>
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

