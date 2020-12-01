using System.Collections.Generic;
using System.Linq;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TagsCloudProcessing.TagsGeneratorFactory
{
    public class TagsGeneratorFactory : ITagsGeneratorFactory
    {
        private readonly Dictionary<string, ITagsGenerator> tagsGenerators;
        private readonly IWordsConfig wordsConfig;

        public TagsGeneratorFactory(IEnumerable<ITagsGenerator> tagsGenerators, IWordsConfig wordsConfig)
        {
            this.tagsGenerators = tagsGenerators.ToDictionary(g => g.GetGeneratorName());
            this.wordsConfig = wordsConfig;
        }

        public ITagsGenerator Create() => tagsGenerators[wordsConfig.TagGeneratorName];

        public IEnumerable<string> GetGeneratorNames() => tagsGenerators.Select(g => g.Key);
    }
}
