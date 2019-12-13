using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly FileHandler fileHandler;
        private readonly ITagCloudBuildingAlgorithm algorithmToBuild;

        public TagCloudBuilder(FileHandler fileHandler,
            ITagCloudBuildingAlgorithm algorithmToBuild)
        {
            this.fileHandler = fileHandler;
            this.algorithmToBuild = algorithmToBuild;
        }

        public IEnumerable<Tag> GetTagsCloud(string fileName)
        {
            var frequencyDict = fileHandler.GetWordsFrequencyDict(fileName);
            return algorithmToBuild.GetTags(frequencyDict);
        }
    }
}