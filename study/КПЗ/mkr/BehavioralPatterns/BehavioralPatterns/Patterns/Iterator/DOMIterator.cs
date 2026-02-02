using System;
using System.Collections;
using System.Collections.Generic;
using BehavioralPatterns.Core;

namespace BehavioralPatterns.Patterns.Iterator
{
    // Ітератор для обходу в глибину
    public class DepthFirstIterator : IEnumerable<LightNode>
    {
        private LightNode _root;

        public DepthFirstIterator(LightNode root)
        {
            _root = root;
        }

        public IEnumerator<LightNode> GetEnumerator()
        {
            var stack = new Stack<LightNode>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                if (current is LightElementNode element)
                {
                    for (int i = element.Children.Count - 1; i >= 0; i--)
                        stack.Push(element.Children[i]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // Ітератор для обходу в ширину
    public class BreadthFirstIterator : IEnumerable<LightNode>
    {
        private LightNode _root;

        public BreadthFirstIterator(LightNode root)
        {
            _root = root;
        }

        public IEnumerator<LightNode> GetEnumerator()
        {
            var queue = new Queue<LightNode>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;

                if (current is LightElementNode element)
                {
                    foreach (var child in element.Children)
                        queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IteratorDemo
    {
        public static void Run(LightElementNode html)
        {
            Console.WriteLine("\n=== 2. Ітератор ===");

            Console.WriteLine("Обхід DOM в глибину (Depth-First):");
            int depthCount = 0;
            foreach (var node in new DepthFirstIterator(html))
            {
                depthCount++;
                if (node is LightTextNode text)
                    Console.WriteLine($"  {depthCount}. Текст: '{text.Text}'");
                else if (node is LightElementNode element)
                    Console.WriteLine($"  {depthCount}. Елемент: <{element.TagName}>");
            }

            Console.WriteLine("\nОбхід DOM в ширину (Breadth-First):");
            int breadthCount = 0;
            foreach (var node in new BreadthFirstIterator(html))
            {
                breadthCount++;
                if (node is LightTextNode text)
                    Console.WriteLine($"  {breadthCount}. Текст: '{text.Text}'");
            }

            Console.WriteLine($"\nВсього елементів: {depthCount}");
        }
    }
}