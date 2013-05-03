using System;
using NUnit.Framework;

namespace ThreeSum
{
	public class ThreeSum
	{
		private int[] data;

		public ThreeSum (int[] data)
		{
			this.data = data;
		}

		public int Count(){
			int n = data.Length;
			int count = 0;
			for (int i = 0; i < n; i++) {
				for (int j = i + 1; j < n; j++) {
					for (int k = j + 1; k < n; k++) {
						if ((data [i] + data [j] + data [k]) == 0) {
							count += 1;
						}
					}
				}
			}
			return count;
		}
	}

	[TestFixture]
	public class ThreeSumTests
	{
		[Test]
		public void Three_sum_test(){
			var input = new[] { 30, -40, -20, -10, 40, 0, 10, 5 };

			var sum = new ThreeSum (input);
			Assert.AreEqual (4, sum.Count ());
		}
	}
}

