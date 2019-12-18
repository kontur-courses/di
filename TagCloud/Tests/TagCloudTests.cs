using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using TagCloudPainter;
using TextPreprocessor.Core;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;

namespace TagCloud.Tests
{
    [TestFixture]
    public class TagCloudTests
    {
        [Test]
        public void TagCloudCorrectWork()
        {
            var fakeTextRider = A.Fake<IFileTextRider>();
            A.CallTo(() => fakeTextRider.CanReadFile()).Returns(true);
            var fakeTextAnalyzer = A.Fake<ITextAnalyzer>();
            var fakeCloudPainter = A.Fake<ITagCloudPainter>();
            
            TagCloudCreator.Create(new []{ fakeTextRider }, fakeTextAnalyzer, fakeCloudPainter);

            A.CallTo(() => fakeTextRider.GetTags()).MustHaveHappened();
            A.CallTo(() => fakeTextAnalyzer.GetTagInfo(A<IEnumerable<Tag>>.Ignored)).MustHaveHappened();
            A.CallTo(() => fakeCloudPainter.Draw(A<IEnumerable<TagInfo>>.Ignored)).MustHaveHappened();
        }
    }
}