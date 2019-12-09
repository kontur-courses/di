using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class WordsParsersFactory : FactoryBase<IWordsParser>
    {
        public WordsParsersFactory(IWordsParser[] parsers, IFactorySettings factorySettings) : 
            base(parsers, factorySettings, s => s.WordsParserId, s => null)
        {}
    }
}