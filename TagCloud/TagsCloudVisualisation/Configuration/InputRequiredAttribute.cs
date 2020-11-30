using System;

namespace TagsCloudVisualisation.Configuration
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class InputRequiredAttribute : Attribute
    {
        public readonly string Description;

        public InputRequiredAttribute(string description)
        {
            this.Description = description;
        }
    }
}