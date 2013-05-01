using System;
using NUnit.Framework;

namespace UnionFind
{
	public class QU
	{
		private int[] items;

		public QU (int n) {
			items = new int[n];
			for (int i = 0; i < items.Length; i++) {
				items[i] = i;
			}
		}

		private int root(int i) {
			while (i != items[i]) {
				i = items [i];
			}
			return i;
		}

		public void union(int p1, int p2) {
			var i = root (p1);
			var j = root (p2);
			items [i] = j;
		}

		public bool connected(int p1, int p2){
			return root (p1) == root(p2);
		}
	}

	[TestFixture]
	public class QUTests{
		private QU qu;

		[SetUp]
		public void Setup(){
			var connections = new ConnectionCollection (){
				{ 4, 3 },
				{ 3, 8 },
				{ 6, 5 },
				{ 9, 4 },
				{ 2, 1 },
			};

			qu = new QU (10);
			foreach (var e in connections) {
				qu.union((int) e.P1,(int) e.P2);
			}

		}

		[Test]
		public void Find_test() {
			Assert.IsFalse (qu.connected (1, 3));
			Assert.IsTrue (qu.connected (8, 4));
		}

		[Test]
		public void Union_test() {
			Assert.IsFalse (qu.connected (1, 6));
			qu.union (1, 6);
			Assert.IsTrue (qu.connected (1, 6));
		}
	}
}

