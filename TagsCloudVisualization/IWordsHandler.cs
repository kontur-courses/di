using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsHandler
    {
        public List<string> HandleWords(List<string> words);
    }
}