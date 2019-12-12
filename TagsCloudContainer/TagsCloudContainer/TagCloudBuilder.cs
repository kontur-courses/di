using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class TagCloudBuilder
    {
        private readonly FileHandler fileHandler;
        private readonly IBuildingAlgorithm algorithmToBuild;

        public TagCloudBuilder(FileHandler fileHandler,
            IBuildingAlgorithm algorithmToBuild)
        {
            this.fileHandler = fileHandler;
            this.algorithmToBuild = algorithmToBuild;
        }

        public IEnumerable<Tag> GetTagClouds(string fileName)
        {
            var frequencyDict = fileHandler.GetWordsFrequencyDict(fileName);
            return algorithmToBuild.GetRectangleSizes(frequencyDict);
        }
    }
}