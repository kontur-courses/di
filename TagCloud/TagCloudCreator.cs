using System;
using System.Linq;
using TagCloudPainter;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;

namespace TagCloud
{
    public static class TagCloudCreator
    {
        public static void Create(
            IFileTextRider[] fileTextRiders,
            ITextAnalyzer textAnalyzer, 
            ITagCloudPainter tagCloudPainter)
        {
            var textRider = GetCorrectTextRider(fileTextRiders);
            
            var tags = textRider.GetTags();
            var tagInfos = textAnalyzer.GetTagInfo(tags);

            tagCloudPainter.Draw(tagInfos);
        }

        private static IFileTextRider GetCorrectTextRider(IFileTextRider[] textRiders)
        {
            var textRider = textRiders.FirstOrDefault(rider => rider.CanReadFile());
            
            if(textRider == null)
                throw new Exception("It is not possible to read a file of this format");

            return textRider;
        }
    }
}