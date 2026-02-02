using System;
using System.Collections.Generic;

namespace BehavioralPatterns.Core
{
    public abstract class LightNode
    {
        public abstract string OuterHTML { get; }
        public abstract string InnerHTML { get; }

        // Шаблонний метод для рендерингу
        public string Render()
        {
            OnCreated();
            OnBeforeRender();
            var result = OuterHTML;
            OnAfterRender();
            return result;
        }

        // Lifecycle hooks (Template Method)
        protected virtual void OnCreated() { }
        protected virtual void OnBeforeRender() { }
        protected virtual void OnAfterRender() { }
        public virtual void OnRemoved() { }
        public virtual void OnStylesApplied() { }
        protected virtual void OnClassListApplied() { }
        protected virtual void OnTextRendered() { }
    }
}