using System.Collections.Generic;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.LayoutContainer.ContainerBuilder
{
    public abstract class AbstractWordsContainerBuilder
    {
        public abstract WordsContainer Build();
        protected abstract WordsContainerBuilder AddWord(Word word, int minCount, int maxCount);

        public AbstractWordsContainerBuilder AddWords(IEnumerable<Word> wordsToBuild, int minCount, int maxCount)
        {
            foreach (var word in wordsToBuild) AddWord(word, minCount, maxCount);
            return this;
        }
    }
}