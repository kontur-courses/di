using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class LinearWeightScaler : IWeightScaler
    {//TODO To settings
        private const int MaxFontSize = 120;
        private const int MinFontSize = 5;

        public IEnumerable<int> Scale(IEnumerable<(string word, int count)> pairs)
        {
            var denominator = pairs.Select(x=>x.count).Max();
            var sizedWords = pairs
                .Select(x =>  MaxFontSize * x.count / denominator)
                .Where(x => x> MinFontSize);
            return sizedWords;
        }
    }
}