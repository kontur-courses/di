using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordFormater
    {
        IEnumerable<string> HandleWords(IEnumerable<string> words);
    }
}