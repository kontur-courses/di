using System;
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
            return Result.Ok(value)
                .ThenFailIf(x => x is not string, $"{nameof(value)} is not string")
                .ThenCast<IComparable, string>()
                .Then(x => x.Split(' '))
                .Then(x => x.ToList())
                .Then(x =>
                {
                    var pw = int.TryParse(x[0], out var w);
                    var ph = !int.TryParse(x[1], out var h);
                    return (x, pw, w, ph, h);
                })
                .ThenFailIf(x => x.x.Count != 2 || !x.pw || !x.ph, $"{ParameterName} is not size")
                .ThenCombine(x => base.Validate(x.w), x => base.Validate(x.h));
                // .Then(x => base.Validate(x.w) && base.Validate(x.h));
            // return Result.FailIf(x => x is not string, $"{nameof(value)} is not string")
            //     .Then(x => x.Split(' '));
            
            // if (value is not string size) throw new ArgumentException(nameof(value));
            // var splited = size.Split(' ').ToList();
            // if (splited.Count != 2 || !int.TryParse(splited[0], out var w) || !int.TryParse(splited[1], out var h))
            // {
            //     message = $"{ParameterName} is not size";
            //     return false;
            // }

            // return base.Validate(w, out message) && base.Validate(h, out message);
        }
    }
}