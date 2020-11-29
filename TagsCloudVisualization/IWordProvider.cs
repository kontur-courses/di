using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordProvider
    {
        public List<string> GetWords(string filepath);
    }
}