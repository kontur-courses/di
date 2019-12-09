using System;

namespace TagsCloudGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FactorialAttribute : Attribute
    {
        public string FactorialId { get; }

        public FactorialAttribute(string factorialId) => FactorialId = factorialId;
    }
}