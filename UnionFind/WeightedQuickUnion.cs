using System;
using NUnit.Framework;

namespace UnionFind
{
	public class WQU
	{
		private int[] items;
		private int[] sizes;

		public WQU (int n) {
			items = new int[n];
			sizes = new int[n];
			for (int i = 0; i < items.Length; i++) {
				items [i] = i;
				sizes [i] = 0;
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

			if (sizes [i] < sizes [j]) {
				items [i] = j;
				sizes [i] += sizes [j];
			}
			else {
				items [j] = i;
				sizes [j] += sizes [i];
			}
		}

		public bool connected(int p1, int p2){
			return root (p1) == root (p2);
		}
	}

	[TestFixture]
	public class WQUTests{
		private WQU wqu;

		[SetUp]
		public void Setup(){
			var connections = new ConnectionCollection (){
				{ 4, 3 },
				{ 3, 8 },
				{ 6, 5 },
				{ 9, 4 },
				{ 2, 1 },
			};

			wqu = new WQU (10);
			foreach (var e in connections) {
				wqu.union((int) e.P1,(int) e.P2);
			}
		}

		[Test]
		public void Find_test() {
			Assert.IsFalse (wqu.connected (1, 3));
			Assert.IsTrue (wqu.connected (8, 4));
		}

		[Test]
		public void Union_test() {
			Assert.IsFalse (wqu.connected (1, 6));
			wqu.union (1, 6);
			Assert.IsTrue (wqu.connected (1, 6));
		}
	}
}

