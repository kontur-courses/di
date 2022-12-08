using TagsCloudContainer.Core.Drawer.Interfaces;
using TagsCloudContainer.Core.TagsClouds.Interfaces;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.TagsClouds
{
    internal class TagsCloudContainer : ITagCloudContainer
    {
        private readonly IRectangleLayout _rectangleLayout;
        private readonly IWordsAnalyzer _wordsAnalyzer;

        public TagsCloudContainer(IWordsAnalyzer wordsAnalyzer, IRectangleLayout rectangleLayout)
        {
            _wordsAnalyzer = wordsAnalyzer;
            _rectangleLayout = rectangleLayout;
        }
        public void CreateTagCloud()
        {
            throw new NotImplementedException();
        }

        public void SaveTagCloud()
        {
            throw new NotImplementedException();
        }
    }
}
