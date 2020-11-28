using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsForCloudGenerator
    {
        List<WordForCloud> Generate(List<string> words);
    }
}