using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class WordsConvertersFactory : FactoryBase<IWordsConverter>
    {
        public WordsConvertersFactory(IWordsConverter[] converters, IFactorySettings factorySettings) :
            base(converters, factorySettings, s => null, s => s.WordsConvertersIds)
        {}
    }
}