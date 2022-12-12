using System.Collections.Generic;

namespace TagsCloudVisualization.Frequency
{
    public interface IFrequencyCounter
    {
        public IEnumerable<string> Elements { get; set; }
        public Dictionary<string, int> GetFrequency();
    }
}
