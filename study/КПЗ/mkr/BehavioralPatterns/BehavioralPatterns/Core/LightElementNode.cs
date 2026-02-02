using System;
using System.Collections.Generic;
using System.Text;

namespace BehavioralPatterns.Core
{
    public class LightElementNode : LightNode
    {
        public string TagName { get; set; }
        public string DisplayType { get; set; } // "block" або "inline"
        public string ClosingType { get; set; } // "single" або "double"
        public List<string> Classes { get; set; } = new List<string>();
        public List<LightNode> Children { get; set; } = new List<LightNode>();

        public LightElementNode(string tagName, string displayType = "block",
                               string closingType = "double")
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosingType = closingType;
            OnCreated();
        }

        public void AddChild(LightNode child)
        {
            Children.Add(child);
            OnInserted();
        }

        public override string OuterHTML
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append($"<{TagName}");

                if (Classes.Count > 0)
                {
                    builder.Append($" class=\"{string.Join(" ", Classes)}\"");
                    OnClassListApplied();
                }

                if (ClosingType == "single")
                {
                    builder.Append(" />");
                }
                else
                {
                    builder.Append(">");
                    builder.Append(InnerHTML);
                    builder.Append($"</{TagName}>");
                }

                return builder.ToString();
            }
        }

        public override string InnerHTML
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var child in Children)
                {
                    builder.Append(child.OuterHTML);
                }
                return builder.ToString();
            }
        }

        // Lifecycle hooks
        protected override void OnCreated()
        {
            Console.WriteLine($"  [Lifecycle] Елемент <{TagName}> створено");
        }

        protected virtual void OnInserted()
        {
            Console.WriteLine($"  [Lifecycle] Додано дочірній елемент до <{TagName}>");
        }

        protected override void OnBeforeRender()
        {
            Console.WriteLine($"  [Lifecycle] Перед рендером <{TagName}>");
        }

        protected override void OnAfterRender()
        {
            Console.WriteLine($"  [Lifecycle] Після рендеру <{TagName}>");
        }

        public override void OnRemoved()
        {
            Console.WriteLine($"  [Lifecycle] Елемент <{TagName}> видалено");
        }

        protected override void OnClassListApplied()
        {
            Console.WriteLine($"  [Lifecycle] Класи застосовано до <{TagName}>: {string.Join(", ", Classes)}");
        }

        protected override void OnTextRendered()
        {
            base.OnTextRendered();
        }
    }
}