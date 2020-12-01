using System;

namespace TagsCloudVisualisation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class VisibleNameAttribute : Attribute
    {
        public string Name { get; }

        public VisibleNameAttribute(string name)
        {
            Name = name;
        }
    }
}