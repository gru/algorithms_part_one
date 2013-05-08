using System;
using NUnit.Framework;

namespace HashTable
{
	public class LinearProbingHashST<TKey, TValue>
	{
		private int m = 30001;
		private TKey[] keys;
		private TValue[] values;

		public LinearProbingHashST ()
		{
			keys = new TKey[m];
			values = new TValue[m];
		}

		public void Add(TKey key, TValue value) 
		{
			int i;
			for (i = GetHash(key); keys[i] != null; i = (i + 1) % m) {
				if (keys [i].Equals (key)) {
					break;
				}
			}
			keys [i] = key;
			values [i] = value;
		}

		public TValue Get(TKey key) 
		{
			for (var i = GetHash(key); keys[i] != null; i = (i + 1) % m) {
				if (keys [i].Equals (key)) {
					return values [i];
				}
			}
			return default(TValue);
		}

		private int GetHash(TKey key)
		{
			return (key.GetHashCode () & 0x7fffffff) % m;
		}
	}

	[TestFixture]
	public class LinearProbingHashTests
	{
		[Test]
		public void Add_get_tests()
		{
			var table = new LinearProbingHashST<string, string> ();
			table.Add ("1", "andrusin");
			table.Add ("2", "andrey");
			Assert.AreEqual ("andrey", table.Get ("2"));
		}
	}
}

