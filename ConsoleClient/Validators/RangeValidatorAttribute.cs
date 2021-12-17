using System;

namespace TagCloudUsageSample.Validators
{
    public class RangeValidatorAttribute : Attribute
    {
        private readonly (IComparable Min, IComparable Max) range;
        protected readonly string ParameterName;
        
        public RangeValidatorAttribute(int min, int max, string parameterName)
        {
            range = (min, max);
            ParameterName = parameterName;
        }

        public virtual bool Validate(IComparable value, out string message)
        {
            var result = Between(value, range);
            message = result ? $"" : $"{ParameterName} should be grate then {range.Min} and less then {range.Max}";
            return result;
        }
        
        private static bool Between(IComparable source, IComparable left, IComparable right)
        {
            var min = left.CompareTo(right) < 0 ? left : right;
            var max = left.CompareTo(right) > 0 ? left : right;

            return source.CompareTo(min) >= 0 && source.CompareTo(max) <= 0;
        }
        
        private static bool Between(IComparable source, (IComparable Min, IComparable Max) range)
        {
            var (min, max) = range;
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("first value should be less or equal then second");

            return Between(source, min, max);
        }
    }
}