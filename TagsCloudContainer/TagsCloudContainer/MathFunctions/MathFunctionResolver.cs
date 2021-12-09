using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.MathFunctions
{
    public class MathFunctionResolver : IMathFunctionResolver
    {
        private readonly Dictionary<MathFunctionType, IMathFunction> mathFunctionResolver;

        public MathFunctionResolver(IEnumerable<IMathFunction> mathFunctions)
        {
            mathFunctionResolver = mathFunctions.ToDictionary(f => f.Type);
        }

        public IMathFunction GetFunction(MathFunctionType type) => mathFunctionResolver[type];
    }
}