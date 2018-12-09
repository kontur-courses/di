using System.Collections.Generic;

namespace TagsCloud
{
    public interface IFrequencyCollection
    {
        ICollection<KeyValuePair<string, double>> GetFrequencyCollection(IEnumerable<string> words);
    }
}