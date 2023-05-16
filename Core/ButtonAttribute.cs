using System;

namespace XIV.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string label;

        public ButtonAttribute() : this("")
        {
            
        }
        
        public ButtonAttribute(string label)
        {
            this.label = label;
        }
    }
}