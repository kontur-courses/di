using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class WordsFiltersFactory : FactoryBase<IWordsFilter>
    {
        public WordsFiltersFactory(IWordsFilter[] filters, IFactorySettings factorySettings) : 
            base(filters, factorySettings, s => null, s => s.WordsFiltersIds)
        {}
    }
}