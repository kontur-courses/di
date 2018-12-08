using System.Collections.Generic;

namespace TagCloud
{
    public interface IWeightScaler
    {
        IEnumerable<int> Scale(IEnumerable<(string word, int count)> pairs);
    }
}