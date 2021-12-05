using System.Collections.Generic;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.LayoutContainer.ContainerBuilder
{
    public interface IWordsContainerBuilder
    {
        WordsContainerBuilder AddWord(Word word, int minCount, int maxCount);
        WordsContainerBuilder AddWords(IEnumerable<Word> wordsToBuild, int minCount, int maxCount);
        WordsContainer Build();
    }
}