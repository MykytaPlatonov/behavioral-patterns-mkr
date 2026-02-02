using System;
using BehavioralPatterns.Core;

namespace BehavioralPatterns.Patterns.TemplateMethod
{
    public class LifecycleDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== 1. Шаблонний метод (Template Method) ===");
            Console.WriteLine("Демонстрація lifecycle hooks:");

            var div = new LightElementNode("div");
            div.Classes.Add("container");
            div.Classes.Add("main");

            var text = new LightTextNode("Hello, World!");
            div.AddChild(text);

            Console.WriteLine("\nРендер елемента:");
            var html = div.Render();
            Console.WriteLine($"Результат: {html}");

            div.OnRemoved();
        }
    }
}