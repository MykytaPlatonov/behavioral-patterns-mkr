using System;
using System.Collections.Generic;
using BehavioralPatterns.Core;

namespace BehavioralPatterns.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class AddElementCommand : ICommand
    {
        private LightElementNode _parent;
        private LightNode _child;
        private bool _isExecuted = false;

        public AddElementCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
        }

        public void Execute()
        {
            if (!_isExecuted)
            {
                _parent.AddChild(_child);
                _isExecuted = true;
                Console.WriteLine($"  [Command] Додано елемент до <{_parent.TagName}>");
            }
        }

        public void Undo()
        {
            if (_isExecuted)
            {
                _parent.Children.Remove(_child);
                _isExecuted = false;
                Console.WriteLine($"  [Command] Видалено елемент з <{_parent.TagName}> (undo)");
            }
        }
    }

    public class RemoveElementCommand : ICommand
    {
        private LightElementNode _parent;
        private LightNode _child;
        private int _index;
        private bool _isExecuted = false;

        public RemoveElementCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
            _index = parent.Children.IndexOf(child);
        }

        public void Execute()
        {
            if (!_isExecuted && _index >= 0)
            {
                _parent.Children.RemoveAt(_index);
                _isExecuted = true;
                Console.WriteLine($"  [Command] Видалено елемент з <{_parent.TagName}>");
            }
        }

        public void Undo()
        {
            if (_isExecuted)
            {
                _parent.Children.Insert(_index, _child);
                _isExecuted = false;
                Console.WriteLine($"  [Command] Відновлено елемент в <{_parent.TagName}> (undo)");
            }
        }
    }

    public class CommandHistory
    {
        private Stack<ICommand> _history = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            command.Execute();
            _history.Push(command);
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine($"  [Command] Історія команд: {_history.Count} операцій");
        }
    }

    public class CommandDemo
    {
        public static void Run(LightElementNode body)
        {
            Console.WriteLine("\n=== 3. Команда ===");

            var history = new CommandHistory();

            // Створюємо новий елемент
            var newParagraph = new LightElementNode("p");
            var newText = new LightTextNode("Новий текст доданий через команду.");
            newParagraph.AddChild(newText);

            // Команда додавання
            var addCommand = new AddElementCommand(body, newParagraph);

            Console.WriteLine("До команди:");
            Console.WriteLine($"  InnerHTML: {body.InnerHTML.Length} символів");

            history.Execute(addCommand);
            Console.WriteLine($"Після команди: {body.InnerHTML.Length} символів");

            // Undo
            history.Undo();
            Console.WriteLine($"Після undo: {body.InnerHTML.Length} символів");

            // Redo (виконати знову)
            history.Execute(addCommand);
            Console.WriteLine($"Після redo: {body.InnerHTML.Length} символів");

            history.ShowHistory();
        }
    }
}