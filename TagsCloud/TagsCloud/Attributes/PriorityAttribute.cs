using System;

namespace TagsCloudGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PriorityAttribute : Attribute
    {
        public int Priority { get; }

        public PriorityAttribute(int priority) => Priority = priority;
    }
}