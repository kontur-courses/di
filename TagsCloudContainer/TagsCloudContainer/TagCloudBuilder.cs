using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly TextHandler fileHandler;
        private readonly ITagCloudBuildingAlgorithm algorithmToBuild;

        public TagCloudBuilder(TextHandler fileHandler,
            ITagCloudBuildingAlgorithm algorithmToBuild)
        {
            this.fileHandler = fileHandler;
            this.algorithmToBuild = algorithmToBuild;
        }

        public IEnumerable<Tag> GetTagsCloud()
        {
            var frequencyDict = fileHandler.GetWordsFrequencyDict();
            return algorithmToBuild.GetTags(frequencyDict);
        }
    }
}