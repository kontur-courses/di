using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TagsCloudProcessing.TagsGeneratorFactory
{
    public class TagsGeneratorFactory : ITagsGeneratorFactory
    {
        private readonly Dictionary<string, Func<ITagsGenerator>> tagsGenerators;
        private readonly IWordsConfig wordsConfig;

        public TagsGeneratorFactory(IWordsConfig wordsConfig)
        {
            tagsGenerators = new Dictionary<string, Func<ITagsGenerator>>();
            this.wordsConfig = wordsConfig;
        }

        public ITagsGenerator Create() => tagsGenerators[wordsConfig.TagGeneratorName]();

        public IEnumerable<string> GetGeneratorNames() => tagsGenerators.Select(g => g.Key);

        public void Register(string generatorName, Func<ITagsGenerator> creationFunc)
        {
            tagsGenerators[generatorName] = creationFunc;
        }
    }
}
