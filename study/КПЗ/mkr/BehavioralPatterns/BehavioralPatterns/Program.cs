using System;
using System.Text;
using BehavioralPatterns.Core;
using BehavioralPatterns.Patterns.TemplateMethod;
using BehavioralPatterns.Patterns.Iterator;
using BehavioralPatterns.Patterns.Command;
using BehavioralPatterns.Patterns.State;
using BehavioralPatterns.Patterns.Visitor;

namespace BehavioralPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== МКР №1: Поведінкові шаблони ===\n");
            Console.WriteLine("Демонстрація 5 поведінкових шаблонів на прикладі HTML двигуна\n");

            // ========== Частина 1: Створення HTML структури (Компонувальник з ЛР3) ==========
            Console.WriteLine("=== Частина 1: Створення HTML структури (Компонувальник) ===");

            var html = new LightElementNode("html");
            var head = new LightElementNode("head");
            var title = new LightElementNode("title");
            title.AddChild(new LightTextNode("МКР №1 - Поведінкові шаблони"));

            var body = new LightElementNode("body");
            var div = new LightElementNode("div", "block", "double");
            div.Classes.Add("container");
            div.Classes.Add("main-content");

            var header = new LightElementNode("header");
            var h1 = new LightElementNode("h1");
            h1.AddChild(new LightTextNode("Модульна контрольна робота №1"));

            var h2 = new LightElementNode("h2");
            h2.AddChild(new LightTextNode("Поведінкові шаблони проєктування"));

            var ul = new LightElementNode("ul");

            var li1 = new LightElementNode("li");
            li1.AddChild(new LightTextNode("Шаблонний метод - Lifecycle hooks для елементів"));

            var li2 = new LightElementNode("li");
            li2.AddChild(new LightTextNode("Ітератор - Обхід DOM дерева в глибину та ширину"));

            var li3 = new LightElementNode("li");
            li3.AddChild(new LightTextNode("Команда - Паттерн Undo/Redo для редагування HTML"));

            var li4 = new LightElementNode("li");
            li4.AddChild(new LightTextNode("Стейт - Різні стани елементів (normal, active, disabled)"));

            var li5 = new LightElementNode("li");
            li5.AddChild(new LightTextNode("Відвідувач - Збір статистики по DOM дереву"));

            ul.AddChild(li1);
            ul.AddChild(li2);
            ul.AddChild(li3);
            ul.AddChild(li4);
            ul.AddChild(li5);

            var description = new LightElementNode("p");
            description.AddChild(new LightTextNode("Ця демонстрація показує реалізацію 5 поведінкових шаблонів на прикладі простого HTML двигуна. Кожен шаблон вирішує конкретну задачу в системі."));

            var footer = new LightElementNode("footer");
            footer.AddChild(new LightTextNode("© 2024 КПЗ. МКР №1. Поведінкові шаблони."));

            // Збираємо структуру
            header.AddChild(h1);
            header.AddChild(h2);
            div.AddChild(header);
            div.AddChild(ul);
            div.AddChild(description);
            body.AddChild(div);
            body.AddChild(footer);
            head.AddChild(title);
            html.AddChild(head);
            html.AddChild(body);

            Console.WriteLine("\nЗгенерована HTML структура:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(html.Render());
            Console.WriteLine(new string('-', 50));

            // ========== Частина 2: Демонстрація 5 поведінкових шаблонів ==========
            Console.WriteLine("\n\n=== Частина 2: Демонстрація поведінкових шаблонів ===\n");

            // 1. Шаблонний метод
            LifecycleDemo.Run();

            // 2. Ітератор
            IteratorDemo.Run(html);

            // 3. Команда
            CommandDemo.Run(body);

            // 4. Стейт
            StateDemo.Run();

            // 5. Відвідувач
            VisitorDemo.Run(html);

            // ========== Частина 3: Висновки ==========
            Console.WriteLine("\n\n=== Частина 3: Висновки ===");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("✅ Успішно продемонстровано 5 поведінкових шаблонів:");
            Console.WriteLine();
            Console.WriteLine("1. ШАБЛОННИЙ МЕТОД (Template Method)");
            Console.WriteLine("   • Реалізовано lifecycle hooks для елементів");
            Console.WriteLine("   • OnCreated(), OnBeforeRender(), OnAfterRender()");
            Console.WriteLine("   • OnClassListApplied(), OnTextRendered(), OnRemoved()");
            Console.WriteLine();
            Console.WriteLine("2. ІТЕРАТОР (Iterator)");
            Console.WriteLine("   • DepthFirstIterator - обхід DOM в глибину");
            Console.WriteLine("   • BreadthFirstIterator - обхід DOM в ширину");
            Console.WriteLine("   • IEnumerable<T> інтерфейс для стандартного foreach");
            Console.WriteLine();
            Console.WriteLine("3. КОМАНДА (Command)");
            Console.WriteLine("   • ICommand інтерфейс з Execute() та Undo()");
            Console.WriteLine("   • AddElementCommand, RemoveElementCommand");
            Console.WriteLine("   • CommandHistory для undo/redo операцій");
            Console.WriteLine();
            Console.WriteLine("4. СТЕЙТ (State)");
            Console.WriteLine("   • IElementState інтерфейс для різних станів");
            Console.WriteLine("   • NormalState, ActiveState, DisabledState, HiddenState");
            Console.WriteLine("   • ElementContext управляє переходами між станами");
            Console.WriteLine();
            Console.WriteLine("5. ВІДВІДУВАЧ (Visitor)");
            Console.WriteLine("   • IHTMLVisitor інтерфейс для відвідування вузлів");
            Console.WriteLine("   • StatisticsVisitor - збір статистики по DOM");
            Console.WriteLine("   • SearchVisitor - пошук елементів за текстом");
            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\nПроект успішно реалізує всі вимоги МКР №1!");
            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}