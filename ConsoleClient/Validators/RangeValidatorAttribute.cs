using System;
using ResultProject;

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

        public virtual Result<bool> Validate(IComparable value)
        {
            return range.AsResult()
                .ThenFailIf(x => x.Min.CompareTo(x.Max) > 0, "first value should be less or equal then second")
                .Then(x => Between(value, x.Min, x.Max))
                .ThenFailIf(x => !x, $"{ParameterName} should be grate then {range.Min} and less then {range.Max}");
        }
        
        private static bool Between(IComparable source, IComparable left, IComparable right)
        {
            var min = left.CompareTo(right) < 0 ? left : right;
            var max = left.CompareTo(right) > 0 ? left : right;

            return source.CompareTo(min) >= 0 && source.CompareTo(max) <= 0;
        }
    }
}