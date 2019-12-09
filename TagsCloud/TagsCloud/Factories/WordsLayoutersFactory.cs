using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class WordsLayoutersFactory : FactoryBase<IWordsLayouter>
    {
        public WordsLayoutersFactory(IWordsLayouter[] wordsLayouters, IFactorySettings factorySettings) :
            base(wordsLayouters, factorySettings, s => s.WordsLayouterId, s => null)
        {}
    }
}