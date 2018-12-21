using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class LinearWeightScaler : IWeightScaler
    {//TODO To settings
        private const int MaxFontSize = 120;
        private const int MinFontSize = 5;

        public IEnumerable<int> Scale(ICollection<(string word, int count)> pairs)=>
            DivideAndCut(pairs, pairs.Select(x => x.count).Max());

        private static IEnumerable<int> DivideAndCut(IEnumerable<(string word, int count)> pairs, int denominator) =>
            pairs.Select(x => MaxFontSize * x.count / denominator)
                .Where(x => x > MinFontSize);
    }
}