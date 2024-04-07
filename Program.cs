using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

public class Node<T>
{
    public T Value { get; set; }
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }

    public Node<T> Parent { get; set; }
    public Node<T> root;
    public Node<T> current;
}

public class BinaryTree<T> : IEnumerable<T>
{
    private Node<T> root;
    private Node<T> current;
public BinaryTree(Node<T> root)
    {
        this.root = root;
        current = GetStartNode();
    }

    private Node<T> GetStartNode()
    {
        Node<T> node = root;
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    private Node<T> GetEndNode()
    {
        Node<T> node = root;
        while (node.Right != null)
        {
            node = node.Right;
        }
        return node;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BinaryTreeEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T Current()
    {
        // Возвращает текущий узел
        if (current != null)
        {
            return current.Value;
        }
        return default(T);
    }

    public T Next()
    {
        // Реализация метода для поиска следующего узла в прямом порядке 
        if (current == null) return default(T);

        if (current.Right != null)
        {
            current = current.Right;
            while (current.Left != null)
            {
                current = current.Left;
            }
        }
        else
        {
            Node<T> temp = current;
            current = current.Parent;
            while (current != null && temp == current.Right)
            {
                temp = current;
                current = current.Parent;
            }
        }

        return current.Value;
    }

    public T Previous()
    {
        // Реализация метода для поиска предыдущего узла в обратном порядке
        if (current == null) return default(T);

        if (current.Left != null)
        {
            current = current.Left;
            while (current.Right != null)
            {
                current = current.Right;
            }
        }
        else
        {
            Node<T> temp = current;
            current = current.Parent;
            while (current != null && temp == current.Left)
            {
                temp = current;
                current = current.Parent;
            }
        }

        return current.Value;
    }

    private class BinaryTreeEnumerator : IEnumerator<T>
    {
        private readonly BinaryTree<T> tree;
        private Node<T> current;

        public BinaryTreeEnumerator(BinaryTree<T> tree)
        {
            this.tree = tree;
            current = tree.GetStartNode();
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (current == null)
                return false;

            tree.Next();

            return true;
        }

        public void Reset()
        {
            current = tree.GetStartNode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем бинарное дерево с узлами типа int
            Node<int> rootNode = new Node<int> { Value = 10 };
            rootNode.Left = new Node<int> { Value = 5 };
            rootNode.Right = new Node<int> { Value = 15 };
            rootNode.Left.Left = new Node<int> { Value = 3 };
            rootNode.Left.Right = new Node<int> { Value = 7 };
            rootNode.Right.Left = new Node<int> { Value = 12 };
            rootNode.Right.Right = new Node<int> { Value = 20 };

            BinaryTree<int> tree = new BinaryTree<int>(rootNode);

            // Используем цикл foreach для обхода дерева
            foreach (var nodeValue in tree)
            {
                Console.WriteLine(nodeValue);
            }

            // Перемещение по дереву с помощью методов Next и Previous
            var current = tree.Current();
            var next = tree.Next();
            var previous = tree.Previous();
            Console.WriteLine("Current: " + current);
            Console.WriteLine("Next: " + next);
            Console.WriteLine("Previous: " + previous);
        }
    }

}