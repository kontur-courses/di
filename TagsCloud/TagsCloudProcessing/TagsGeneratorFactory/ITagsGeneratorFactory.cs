using System;
using System.Collections.Generic;
using TagsCloud.TagsCloudProcessing.TegsGenerators;

namespace TagsCloud.TagsCloudProcessing.TagsGeneratorFactory
{
    public interface ITagsGeneratorFactory
    {
        ITagsGenerator Create();
        IEnumerable<string> GetGeneratorNames();
        void Register(string generatorName, Func<ITagsGenerator> creationFunc);
    }
}
