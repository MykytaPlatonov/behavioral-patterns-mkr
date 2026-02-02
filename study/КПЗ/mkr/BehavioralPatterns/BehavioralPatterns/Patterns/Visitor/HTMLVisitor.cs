using System;
using System.Collections.Generic;
using BehavioralPatterns.Core;

namespace BehavioralPatterns.Patterns.Visitor
{
    public interface IHTMLVisitor
    {
        void Visit(LightTextNode textNode);
        void Visit(LightElementNode elementNode);
    }

    public class StatisticsVisitor : IHTMLVisitor
    {
        public int TextNodesCount { get; private set; }
        public int ElementNodesCount { get; private set; }
        public int TotalCharacters { get; private set; }
        public Dictionary<string, int> TagStatistics { get; private set; } = new Dictionary<string, int>();

        public void Visit(LightTextNode textNode)
        {
            TextNodesCount++;
            TotalCharacters += textNode.Text.Length;
            Console.WriteLine($"  [Visitor] Текстовий вузол: '{textNode.Text.Substring(0, Math.Min(20, textNode.Text.Length))}...' ({textNode.Text.Length} символів)");
        }

        public void Visit(LightElementNode elementNode)
        {
            ElementNodesCount++;

            // Збираємо статистику по тегах
            if (TagStatistics.ContainsKey(elementNode.TagName))
                TagStatistics[elementNode.TagName]++;
            else
                TagStatistics[elementNode.TagName] = 1;

            Console.WriteLine($"  [Visitor] Елемент <{elementNode.TagName}> з {elementNode.Children.Count} дітьми, класи: {string.Join(", ", elementNode.Classes)}");
        }

        public void PrintStatistics()
        {
            Console.WriteLine("\n=== Статистика відвідувача ===");
            Console.WriteLine($"Текстові вузли: {TextNodesCount}");
            Console.WriteLine($"Елементні вузли: {ElementNodesCount}");
            Console.WriteLine($"Загальна кількість вузлів: {TextNodesCount + ElementNodesCount}");
            Console.WriteLine($"Всього символів тексту: {TotalCharacters}");
            Console.WriteLine("\nСтатистика по тегах:");
            foreach (var tag in TagStatistics)
            {
                Console.WriteLine($"  <{tag.Key}>: {tag.Value} шт.");
            }
        }
    }

    public class SearchVisitor : IHTMLVisitor
    {
        private string _searchText;
        private List<LightNode> _results = new List<LightNode>();

        public SearchVisitor(string searchText)
        {
            _searchText = searchText.ToLower();
        }

        public void Visit(LightTextNode textNode)
        {
            if (textNode.Text.ToLower().Contains(_searchText))
            {
                _results.Add(textNode);
                Console.WriteLine($"  [Search] Знайдено текст: '{textNode.Text}'");
            }
        }

        public void Visit(LightElementNode elementNode)
        {
            if (elementNode.TagName.ToLower().Contains(_searchText) ||
                elementNode.Classes.Exists(c => c.ToLower().Contains(_searchText)))
            {
                _results.Add(elementNode);
                Console.WriteLine($"  [Search] Знайдено елемент: <{elementNode.TagName}>");
            }
        }

        public List<LightNode> GetResults() => _results;
    }

    // Клас для відвідування вузлів
    public static class VisitorExtensions
    {
        public static void Accept(this LightNode node, IHTMLVisitor visitor)
        {
            if (node is LightTextNode textNode)
                visitor.Visit(textNode);
            else if (node is LightElementNode elementNode)
            {
                visitor.Visit(elementNode);
                foreach (var child in elementNode.Children)
                    child.Accept(visitor);
            }
        }
    }

    public class VisitorDemo
    {
        public static void Run(LightElementNode html)
        {
            Console.WriteLine("\n=== 5. Відвідувач ===");

            // Статистичний відвідувач
            Console.WriteLine("\nСтатистичний відвідувач:");
            var statsVisitor = new StatisticsVisitor();
            html.Accept(statsVisitor);
            statsVisitor.PrintStatistics();

            // Пошуковий відвідувач
            Console.WriteLine("\nПошуковий відвідувач (пошук 'text'):");
            var searchVisitor = new SearchVisitor("text");
            html.Accept(searchVisitor);
            Console.WriteLine($"Знайдено результатів: {searchVisitor.GetResults().Count}");
        }
    }
}