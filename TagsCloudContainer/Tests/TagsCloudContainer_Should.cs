using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using FakeItEasy;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TagsCloudContainer_Should
    {
        private ITextReader textReader;
        private IWordsFilter wordsFilter;
        private IWordsCounter wordsCounter;
        private IWordsToSizesConverter wordsToSizesConverter;
        private ICloudLayouter ccl;
        private IVisualiser visualiser;
        private IFileSaver imageSaver;

        [SetUp]
        public void SetUp()
        {
            textReader = A.Fake<ITextReader>();
            wordsFilter = A.Fake<IWordsFilter>();
            wordsCounter = A.Fake<IWordsCounter>();
            wordsToSizesConverter = A.Fake<IWordsToSizesConverter>();
            ccl = A.Fake<ICloudLayouter>();
            visualiser = A.Fake<IVisualiser>();
            imageSaver = A.Fake<IFileSaver>();
        }

        private void ConfigureDefaultFakes()
        {
            A.CallTo(() => textReader.Read(A<string>.Ignored)).Returns(new[] {"t", "y", "f"});
            A.CallTo(() => wordsFilter.FilterWords(A<IEnumerable<string>>.Ignored)).Returns(new[] {"t", "y"});
            A.CallTo(() => wordsCounter.CountWords(A<IEnumerable<string>>.Ignored))
                .Returns(new Dictionary<string, int>() {{"t", 1}, {"y", 1}});
            A.CallTo(() => wordsToSizesConverter.GetSizesOf(A<Dictionary<string, int>>.Ignored)).Returns(
                new[] {("t", new Size(50, 50)), ("y", new Size(50, 50))}
            );
            A.CallTo(() => ccl.PutNextRectangle(A<Size>.Ignored)).Returns(new Rectangle());
            A.CallTo(() => visualiser.DrawRectangles(A<ICloudLayouter>.Ignored, A<(string, Size)[]>.Ignored))
                .Returns(new Bitmap(500, 500));
            A.CallTo(() => imageSaver.Save(A<Bitmap>.Ignored, A<string>.Ignored)).DoesNothing();
        }

        [Test]
        public void CallNecessaryMethods()
        {
            var app = new TagsCloudContainer(
                textReader,
                wordsFilter,
                wordsCounter,
                wordsToSizesConverter,
                ccl,
                visualiser,
                imageSaver,
                "",
                ""
            );

            ConfigureDefaultFakes();
            app.Perform();

            A.CallTo(() => textReader.Read(A<string>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => wordsFilter.FilterWords(A<IEnumerable<string>>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => wordsCounter.CountWords(A<IEnumerable<string>>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => wordsToSizesConverter.GetSizesOf(A<Dictionary<string, int>>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => visualiser.DrawRectangles(A<ICloudLayouter>.Ignored, A<(string, Size)[]>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => imageSaver.Save(A<Bitmap>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}