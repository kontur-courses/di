using TagsCloudContainer.Core.Drawer.Interfaces;
using TagsCloudContainer.Core.TagsClouds.Interfaces;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.TagsClouds
{
    public class TagsCloud : ITagsCloud
    {
        private readonly IWordsAnalyzer _wordsAnalyzer;
        private readonly IRectangleLayout _rectangleLayout;

        public TagsCloud(IWordsAnalyzer wordsAnalyzer, IRectangleLayout rectangleLayout)
        {
            _wordsAnalyzer = wordsAnalyzer;
            _rectangleLayout = rectangleLayout;
        }

        public void CreateTagCloud()
        {
            _rectangleLayout.PlaceWords(_wordsAnalyzer.AnalyzeWords());
            _rectangleLayout.DrawLayout();
        }

        public void SaveTagCloud()
        {
            _rectangleLayout.SaveLayout();
        }
    }
}