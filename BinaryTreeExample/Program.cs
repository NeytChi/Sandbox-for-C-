using System;
using System.Collections.Generic;

namespace BinaryTreeExample
{
    internal class Program
    {
        static Action<Node> hello = node => Console.WriteLine(node.ToString());

        static void Main(string[] args)
        {
            Console.WriteLine("Depth First Search for binary tree algorithm: ");
            DFSWriteLine();
            Console.WriteLine("Breadth First Search for binary tree algorithm: ");
            BFSWriteLine();
            Console.Read();
        }
        static void DFSWriteLine()
        {
            var tree = new BinaryTree(hello);

            var n1 = new Node { Data = "First Node" };
            var n2 = new Node { Data = "Second Node" };
            var n3 = new Node { Data = "Third Node" };
            var n4 = new Node { Data = "Fourth Node" };
            var n5 = new Node { Data = "Fiveth Node" };
            var n6 = new Node { Data = "Sixth Node" };
            n1.Right = n2;
            n2.Left = n3;
            n3.Right = n4;
            n3.Left = n5;
            n5.Right = n6;

            tree.DFS(n1);
        }
        static void BFSWriteLine()
        {
            var tree = new BinaryTree(hello);

            var n1 = new Node { Data = "First Node" };
            var n2 = new Node { Data = "Second Node" };
            var n3 = new Node { Data = "Third Node" };
            var n4 = new Node { Data = "Fourth Node" };
            var n5 = new Node { Data = "Fiveth Node" };
            var n6 = new Node { Data = "Sixth Node" };
            n1.Right = n2;
            n1.Left = n3;
            n3.Left = n4;
            n3.Right = n5;
            n4.Right = n6; 

            tree.Root = n1;
            tree.BFS(n1);
        }
    }
    public class BinaryTree
    {
        public delegate void BinaryTreeDelegate(Node tree);
        public Action<Node> @delegate { get; set; }
        public Node Root { get; set; }
        public Node Leaf { get; set; }

        public BinaryTree(Action<Node> treeDelegate)
        {
            @delegate = treeDelegate;
        }
        public void DFS(Node root)
        {
            if (root != null)
            {
                @delegate(root);
                DFS(root.Right); 
                DFS(root.Left);
            }
        }
        public void BFS(Node root)
        {
            if (root == null)
                return;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                @delegate(node);
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
    }
    public class Node
    {
        public string Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public override string ToString()
        {
            return Data;
        }
    }
}
