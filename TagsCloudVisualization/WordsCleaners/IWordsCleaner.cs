using System.Collections.Generic;

namespace TagsCloudVisualization.WordsCleaners
{
    public interface IWordsCleaner
    {
        public List<string> CleanWords(List<string> words);
    }
}