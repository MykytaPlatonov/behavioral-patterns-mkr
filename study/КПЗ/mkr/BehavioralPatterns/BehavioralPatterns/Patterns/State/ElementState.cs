using System;
using BehavioralPatterns.Core;

namespace BehavioralPatterns.Patterns.State
{
    public interface IElementState
    {
        string ApplyStyle(LightElementNode element);
        string HandleClick(LightElementNode element);
        string GetStateName();
    }

    public class NormalState : IElementState
    {
        public string ApplyStyle(LightElementNode element)
        {
            element.Classes.Remove("disabled");
            element.Classes.Remove("active");
            element.Classes.Remove("hidden");
            return "normal";
        }

        public string HandleClick(LightElementNode element)
        {
            Console.WriteLine($"  [State] Клік по нормальному елементу <{element.TagName}>");
            return "clicked";
        }

        public string GetStateName() => "Normal";
    }

    public class DisabledState : IElementState
    {
        public string ApplyStyle(LightElementNode element)
        {
            if (!element.Classes.Contains("disabled"))
                element.Classes.Add("disabled");
            element.Classes.Remove("active");
            return "disabled";
        }

        public string HandleClick(LightElementNode element)
        {
            Console.WriteLine($"  [State] Елемент <{element.TagName}> вимкнений, клік ігнорується");
            return "ignored";
        }

        public string GetStateName() => "Disabled";
    }

    public class ActiveState : IElementState
    {
        public string ApplyStyle(LightElementNode element)
        {
            if (!element.Classes.Contains("active"))
                element.Classes.Add("active");
            element.Classes.Remove("disabled");
            return "active";
        }

        public string HandleClick(LightElementNode element)
        {
            Console.WriteLine($"  [State] Активний клік по <{element.TagName}>");
            return "active-click";
        }

        public string GetStateName() => "Active";
    }

    public class HiddenState : IElementState
    {
        public string ApplyStyle(LightElementNode element)
        {
            if (!element.Classes.Contains("hidden"))
                element.Classes.Add("hidden");
            return "hidden";
        }

        public string HandleClick(LightElementNode element)
        {
            Console.WriteLine($"  [State] Прихований елемент <{element.TagName}>, клік неможливий");
            return "impossible";
        }

        public string GetStateName() => "Hidden";
    }

    // Контекст, що управляє станами
    public class ElementContext
    {
        private IElementState _currentState;
        private LightElementNode _element;

        public ElementContext(LightElementNode element)
        {
            _element = element;
            _currentState = new NormalState();
        }

        public void SetState(IElementState state)
        {
            _currentState = state;
            Console.WriteLine($"  [State] Змінено стан на: {state.GetStateName()}");
            _currentState.ApplyStyle(_element);
        }

        public void Click()
        {
            var result = _currentState.HandleClick(_element);
            Console.WriteLine($"  [State] Результат кліку: {result}");
        }

        public void Render()
        {
            Console.WriteLine($"  [State] Рендер елемента в стані '{_currentState.GetStateName()}':");
            Console.WriteLine($"    {_element.OuterHTML}");
        }
    }

    public class StateDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== 4. Стейт ===");

            var button = new LightElementNode("button");
            button.AddChild(new LightTextNode("Натисни мене"));

            var context = new ElementContext(button);

            // Тестуємо різні стани
            context.Render();
            context.Click();

            context.SetState(new ActiveState());
            context.Render();
            context.Click();

            context.SetState(new DisabledState());
            context.Render();
            context.Click();

            context.SetState(new HiddenState());
            context.Render();
            context.Click();

            // Повертаємо до нормального стану
            context.SetState(new NormalState());
            context.Render();
        }
    }
}