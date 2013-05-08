using System;
using NUnit.Framework;

namespace HashTable
{
	public class SeparateHashChainingHashST<TKey, TValue>
	{
		private int m = 97;
		private Node[] st;

		public SeparateHashChainingHashST ()
		{
			st = new Node[m];
		}

		public void Add(TKey key, TValue value) 
		{
			var i = GetHash (key);
			for (Node node = st[i]; node != null; node = node.next) {
				if (key.Equals(node.key)) {
					node.value = value;
					return;
				}
			}
			st[i] = new Node(key, value, st[i]);
		}

		public TValue Get(TKey key) 
		{
			var i = GetHash (key);
			for (Node node = st[i]; node != null; node = node.next) {
				if (key.Equals (node.key)) {
					return node.value;
				}
			}
			return default(TValue);
		}

		private int GetHash(TKey key)
		{
			return (key.GetHashCode () & 0x7fffffff) % m;
		}
	
		private class Node
		{
			public TKey key;
			public TValue value;
			public Node next;

			public Node (TKey key, TValue value, Node next)
			{
				this.key = key;
				this.value = value;
				this.next = next;
			}
		}
	}

	[TestFixture]
	public class HashTableTests
	{
		[Test]
		public void Add_get_tests()
		{
			var table = new SeparateHashChainingHashST<string, string> ();
			table.Add ("1", "2");
			table.Add ("2", "5");
			Assert.AreEqual ("2", table.Get ("1"));
			Assert.AreEqual ("5", table.Get ("2"));
		}
	}
}