using System.Collections.Generic;

namespace TagsCloudVisualization.Infrastructure
{
    public interface IWordsReader
    {
        public IEnumerable<string> GetAllData(string source);
    }
}