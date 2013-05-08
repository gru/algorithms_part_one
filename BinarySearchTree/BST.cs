using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;
using System.Linq;

namespace BinarySearchTree
{
	public class BST<TKey, TValue> : IEnumerable<TKey>
		where TKey : IComparable<TKey> 
	{
		private Node root;

		public void Add(TKey key, TValue value)
		{
			root = Put (root, key, value);
		}

		public TValue Get(TKey key)
		{
			var node = root;
			while (node != null) {
				var cmp = node.key.CompareTo (key);
				if (cmp > 0) {
					node = node.left;
				} else if (cmp < 0) {
					node = node.right;
				} else {
					return node.value;
				}
			}
			return default(TValue);
		}

		public IEnumerable<TKey> GetKeys(TKey lo, TKey hi)
		{
			// TODO: implement
			yield break;
		}

		public void Delete(TKey key)
		{
			root = Delete (root, key);
		}

		public TKey MinimumKey()
		{
			Node node = Min(root);
			if (node == null) {
				return default(TKey);
			}
			return node.key;
		}

		public TKey MaximumKey()
		{
			Node node = Max (root);
			if (node == null) {
				return default(TKey);
			}
			return node.key;
		}

		public TKey Floor (TKey key)
		{
			var node = Floor (root, key);
			if (node == null)
				return default(TKey);
			return node.key;
		}

		public TKey Ceiling (TKey key)
		{
			var node = Ceiling (root, key);
			if (node == null)
				return default(TKey);
			return node.key;
		}

		public int Count()
		{
			return Count (root);	
		}

		// 11 - 1
		public int Count(TKey lo, TKey hi)
		{
			if (Contains (hi)) {
				return Rank (hi) - Rank (lo) + 1;
			} else {
				return Rank (hi) - Rank (lo);
			}
		}

		public int Rank(TKey key) 
		{
			return Rank (root, key);
		}

		public void DeleteMinimum ()
		{
			root = DeleteMinimum (root);
		}

		public void DeleteMaximum()
		{
			root = DeleteMaximum (root);
		}

		public bool Contains(TKey key)
		{
			var value = Get (key);
			var def = default(TValue);
			if (def == null) {
				return value != null;
			}
			return !def.Equals(value);
		}

		// Hibbard deletion
		private Node Delete(Node node, TKey key)
		{
			if (node == null) {
				return null;
			}
			int cmp = key.CompareTo (node.key);
			if (cmp < 0) {
				node.left = Delete (node.left, key);
			} else if (cmp > 0) {
				node.right = Delete (node.right, key);
			} else {
				if (node.right == null) {
					return node.left;
				}
				var t = node;
				node = Min (t.right);
				node.right = DeleteMinimum (t.right);
				node.left = t.left;
			}
			node.count = Count (node.left) + Count (node.right) + 1;
			return node;
		}

		private Node Min(Node node)
		{
			if (node == null) {
				return null;
			}
			while (node.left != null) {
				node = node.left; 
			}
			return node;
		}

		private Node Max(Node node)
		{
			if (node == null) {
				return null;
			}
			while (node.right != null) {
				node = node.right; 
			}
			return node;
		}

		private Node DeleteMaximum(Node node)
		{
			if (node == null) {
				return null;
			}
			if (node.right == null) {
				return node.left;
			}
			node.right = DeleteMinimum (node.right);
			node.count = 1 + Count (node.left) + Count (node.right);
			return node;
		}

		private Node DeleteMinimum(Node node)
		{
			if (node == null) {
				return null;
			}
			if (node.left == null) {
				return node.right;
			}
			node.left = DeleteMinimum (node.left);
			node.count = 1 + Count (node.left) + Count (node.right);
			return node;
		}

		private int Rank(Node node, TKey key)
		{
			if (node == null) {
				return 0;
			}

			var cmp = key.CompareTo (node.key);
			if (cmp > 0) {
				return 1 + Count (node.left) + Rank (node.right, key);
			} else if (cmp < 0) {
				return Rank (node.left, key);
			} else {
				return Count(node.left);
			}
		}

		private int Count(Node node)
		{
			if (node == null) {
				return 0;
			}
			return node.count;
		}

		private Node Floor (Node node, TKey key)
		{
			if (node == null) {
				return null;
			}

			var cmp = key.CompareTo (node.key);
			if (cmp == 0) {
				return node;
			} else if (cmp < 0) {
				return Floor (node.left, key);
			} else {
				var right = Floor (node.right, key);
				if (right != null) {
					return right;
				}
				return node;
			}
		}

		private Node Ceiling(Node node, TKey key)
		{
			if (node == null) {
				return null;
			}

			var cmp = key.CompareTo (node.key);
			if (cmp == 0) {
				return node;
			} else if (cmp > 0) {
				return Floor (node.right, key);
			} else {
				var left = Floor (node.left, key); 
				if (left != null) {
					return left;
				}
				return node;
			}
		}

		private Node Put(Node node, TKey key, TValue value)
		{
			if (node == null) { 
				return new Node (key, value, 1);
			}
			var cmp = node.key.CompareTo (key);
			if (cmp < 0) {
				node.right = Put (node.right, key, value);
			} else if (cmp > 0) {
				node.left = Put (node.left, key, value);
			} else {
				node.value = value;
			}
			node.count = 1 + Count (node.left) + Count (node.right);
			return node;
		}

		private void InOrder(Node node, Queue<TKey> queue)
		{
			if (node == null) { 
				return;
			}
			InOrder (node.left, queue);
			queue.Enqueue (node.key);
			InOrder (node.right, queue);
		}

		#region IEnumerable implementation

		public IEnumerator<TKey> GetEnumerator ()
		{
			var queue = new Queue<TKey> (Count());
			InOrder (root, queue);
			return queue.GetEnumerator();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion

		[DebuggerDisplay("{key}")]
		private class Node
		{
			public TKey key;
			public TValue value;
			public Node left;
			public Node right;
			public int count;

			public Node (TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}

			public Node (TKey key, TValue value, int count)
				: this(key, value)
			{
				this.count = count;
			}
		}
	}

	[TestFixture]
	public class BinarySearchTreeTests
	{
		[Test]
		public void Search_test() 
		{
			var tree = new BST<string, string> {
				{ "1", "andrey" },
				{ "2", "andrushin" }
			};

			Assert.AreEqual ("andrushin", tree.Get ("2"));
		}

		[Test]
		public void Delete_min_max_test()
		{
			var tree = new BST<string, string> {
				{ "1", "andrey" },
				{ "2", "andrushin" }
			};

			Assert.IsTrue (tree.Contains ("1"));
			tree.DeleteMinimum ();
			Assert.IsFalse (tree.Contains ("1"));
			tree.DeleteMaximum ();
			Assert.IsFalse (tree.Contains ("2"));
		}

		[Test]
		public void Max_min_test() 
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};
			Assert.AreEqual (1, tree.MinimumKey ());
			Assert.AreEqual (5, tree.MaximumKey ());
		}

		[Test]
		public void Floor_ceiling_test() 
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};

			Assert.AreEqual (5, tree.Floor (6));
			Assert.AreEqual (3, tree.Ceiling (3));
		}

		[Test]
		public void Count_test() 
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};

			Assert.AreEqual (5, tree.Count());
		}

		[Test]
		public void Rank_test()
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};

			Assert.AreEqual (2, tree.Rank (3));
		}

		[Test]
		public void Iterate_in_order_test() 
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};

			Assert.AreEqual (new[] { 1, 2, 3, 4, 5 }, tree);
		}

		[Test]
		public void Delete_test()
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};
			Assert.IsTrue (tree.Contains (1));
			tree.Delete (1);
			Assert.IsFalse (tree.Contains (1));
		}

		[Test]
		public void Count_keys_test() 
		{
			var tree = new BST<int, int> {
				{ 1, 100 },
				{ 2, 200 },
				{ 3, 0 },
				{ 5, -10 },
				{ 4, 100 }
			};
			Assert.AreEqual(2, tree.Count(1, 2));
		}
	}
}