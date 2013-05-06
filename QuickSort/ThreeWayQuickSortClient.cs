using System;
using NUnit.Framework;

namespace QuickSort
{
	public class ThreeWayQuickSortClient
	{
		public IComparable[] Sort(IComparable[] data)
		{
			return Sort (data, 0, data.Length - 1);
		}

		public IComparable[] Sort(IComparable[] data, int lo, int hi)
		{
			if (hi <= lo) {
				return data;
			}
			int lt = lo;
			int gt = hi;
			int i = lo;
			var v = data [lo];
			while (i <= gt) {
				var cmp = data [i].CompareTo (v);
				if (cmp < 0) {
					Exchange(data, lt++, i++);
				} else if (cmp > 0) {
					Exchange (data, i, gt--);
				} else {
					i++;
				}
			}

			Sort (data, lo, lt - 1);
			Sort (data, gt + 1, hi);

			return data;
		}

		private void Exchange(IComparable[] data, int i, int j) {
			var a = data [i];
			data [i] = data [j];
			data [j] = a;
		}
	}

	[TestFixture]
	public class TreeWayQuickSortTests
	{
		[Test]
		public void Three_way_quick_sort_tests() {
			var data = new [] { "a", "b", "c", "d", "a", "x", "c", "x", "9", "8", "1" };
			var client = new ThreeWayQuickSortClient ();
			Assert.AreEqual (new [] { "1", "8", "9", "a", "a", "b", "c", "c", "d", "x", "x" }, client.Sort (data));
		}
	}
}

