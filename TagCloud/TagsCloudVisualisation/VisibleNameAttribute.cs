using System;

namespace TagsCloudVisualisation
{
    [Obsolete("You should generate mapping in UI")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class VisibleNameAttribute : Attribute
    {
        public string Name { get; }

        public VisibleNameAttribute(string name)
        {
            Name = name;
        }
    }
}