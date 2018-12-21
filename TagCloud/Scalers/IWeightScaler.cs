using System.Collections.Generic;

namespace TagCloud
{
    public interface IWeightScaler
    {
        IEnumerable<int> Scale(ICollection<(string word, int count)> pairs);
    }
}