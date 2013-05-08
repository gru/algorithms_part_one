using System;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;

namespace BalancedSearchTree
{
	public class RedBlackTree<TKey, TValue> : IEnumerable<TKey>
		where TKey : IComparable
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
				var cmp = key.CompareTo (node.key);
				if (cmp > 0) {
					node = node.right;
				} else if (cmp < 0) {
					node = node.left;
				} else {
					return node.value;
				}
			}
			return default(TValue);
		}

		private Node Put(Node node, TKey key, TValue value)
		{
			if (node == null) { 
				return new Node (key, value, Color.Red);
			}
			var cmp = node.key.CompareTo (key);
			if (cmp < 0) {
				node.right = Put (node.right, key, value);
			} else if (cmp > 0) {
				node.left = Put (node.left, key, value);
			} else {
				node.value = value;
			}

			if (IsRed(node.right) && !IsRed(node.left)) {
				node = RotateLeft (node);
			}
			if (IsRed (node.left) && IsRed (node.left.left)) {
				node = RotateRight (node);
			}
			if (IsRed(node.left) && IsRed(node.right)) {
				FlipColors (node);
			}

			return node;
		}

		private Node RotateLeft(Node node)
		{
			Assert.IsTrue (IsRed (node.right));

			var right = node.right;
			node.right = node.left;
			right.left = node;
			right.color = node.color;
			node.color = Color.Red;
			return right;
		}

		private Node RotateRight(Node node)
		{
			Assert.IsTrue (IsRed (node.left));

			var left = node.left;
			node.left = node.right;
			left.right = node;
			left.color = node.color;
			node.color = Color.Red;
			return left;
		}

		private bool IsRed(Node node)
		{
			if (node == null) {
				return false;
			}
			return node.color == Color.Red;
		}

		private void FlipColors(Node node)
		{
			Assert.IsFalse (IsRed (node));
			Assert.IsTrue (IsRed (node.left));
			Assert.IsTrue (IsRed (node.right));

			node.color = Color.Red;
			node.right.color = Color.Black;
			node.left.color = Color.Black;
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

		private class Node
		{
			public TKey key;
			public TValue value;
			public Node left;
			public Node right;
			public Color color;

			public Node (TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}

			public Node (TKey key, TValue value, Color color)
				: this(key, value)
			{
				this.color = color;
			}
		}

		private enum Color
		{
			Black,
			Red
		}

		#region IEnumerable implementation

		public IEnumerator<TKey> GetEnumerator ()
		{
			var queue = new Queue<TKey> ();
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
	}

	[TestFixture]
	public class RedBlackTreeTests
	{
		[Test]
		public void Get_test()
		{
			var tree = new RedBlackTree<string, string> {
				{"1", "andrey"},
				{"2", "andrushin"}
			};

			Assert.AreEqual("andrey", tree.Get("1"));
			Assert.AreEqual ("andrushin", tree.Get ("2"));
		}
	}
}