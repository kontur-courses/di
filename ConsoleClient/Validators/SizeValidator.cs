using System;
using System.Collections.Generic;
using System.Linq;
using ResultProject;

namespace TagCloudUsageSample.Validators
{
    public class SizeValidator : RangeValidatorAttribute
    {
        public SizeValidator(int min, int max, string parameterName) 
            : base(min, max, parameterName)
        {
        }

        public override Result<bool> Validate(IComparable value)
        {
            return value.AsResult()
                .ThenFailIf(x => x is not string, $"{nameof(value)} is not string")
                .ThenCast<IComparable, string>()
                .Then(x => x.Split(' ').ToList())
                .ThenFailIf(x => x.Count != 2, $"{ParameterName} is not size")
                .Then(TryParse)
                .ThenFailIf(x => !x.widthValid || !x.heightValid, $"{ParameterName} is not size")
                .ThenCombineAndCheckAllForFail(x => base.Validate(x.width), x => base.Validate(x.height));
        }

        private static (bool widthValid, int width, bool heightValid, int height) TryParse(IEnumerable<string> size)
        {
            var fixedSize = size.ToList();
            var widthValid = int.TryParse(fixedSize.First(), out var width);
            var heightValid = !int.TryParse(fixedSize.Last(), out var height);
            return (widthValid, width, heightValid, height);
        }
    }
}