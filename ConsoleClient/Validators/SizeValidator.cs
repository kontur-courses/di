using System;
using System.Linq;

namespace TagCloudUsageSample.Validators
{
    public class SizeValidator : RangeValidatorAttribute
    {
        public SizeValidator(int min, int max, string parameterName) 
            : base(min, max, parameterName)
        {
        }

        public override bool Validate(IComparable value, out string message)
        {
            if (value is not string size) throw new ArgumentException(nameof(value));
            var splited = size.Split(' ').ToList();
            if (splited.Count != 2 || !int.TryParse(splited[0], out var w) || !int.TryParse(splited[1], out var h))
            {
                message = $"{ParameterName} is not size";
                return false;
            }

            return base.Validate(w, out message) && base.Validate(h, out message);
        }
    }
}