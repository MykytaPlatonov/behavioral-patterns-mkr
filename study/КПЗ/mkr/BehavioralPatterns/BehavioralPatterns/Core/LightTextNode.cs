using System;

namespace BehavioralPatterns.Core
{
    public class LightTextNode : LightNode
    {
        public string Text { get; set; }

        public LightTextNode(string text)
        {
            Text = text;
            OnCreated();
        }

        public override string OuterHTML => Text;
        public override string InnerHTML => Text;

        protected override void OnTextRendered()
        {
            Console.WriteLine($"  [Lifecycle] Текст відрендерено: '{Text}'");
        }
    }
}