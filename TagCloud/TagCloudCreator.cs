using TagCloudPainter;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;

namespace TagCloud
{
    public static class TagCloudCreator
    {
        public static void Create(IFileTextRider fileTextRider, ITextAnalyzer textAnalyzer, ITagCloudPainter tagCloudPainter)
        {
            var tags = fileTextRider.GetTags();
            var tagInfos = textAnalyzer.GetTagInfo(tags);

            tagCloudPainter.Draw(tagInfos);
        }
    }
}