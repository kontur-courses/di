using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsGenerator
    {
        IEnumerable<IWord> GenerateWords();
    }
}