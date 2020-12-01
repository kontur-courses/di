using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsCleaner
    {
        public List<string> CleanWords(List<string> words);
    }
}