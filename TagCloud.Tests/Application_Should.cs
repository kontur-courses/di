using System.Collections.Generic;
using System.Drawing;
using FakeItEasy;
using NUnit.Framework;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;
using TagCloudCreator;
using Size = TagCloud.Layouter.Size;

namespace TagCloud.Tests
{
    [TestFixture]
    public class Application_Should
    {
        [SetUp]
        public void SetUp()
        {
            fileReader = A.Fake<IFileReader>();
            imageSaver = A.Fake<IImageSaver>();
            layouter = A.Fake<ICloudLayouter>();
            sizeScheme = A.Fake<ISizeScheme>();
            statisticsCollector = A.Fake<IStatisticsCollector>();
            visualizer = A.Fake<IVisualizer>();
            wordFilter = A.Fake<IWordFilter>();
            wordProcessor = A.Fake<IWordProcessor>();
        }

        private IFileReader fileReader;
        private IImageSaver imageSaver;
        private ICloudLayouter layouter;
        private ISizeScheme sizeScheme;
        private IStatisticsCollector statisticsCollector;
        private IVisualizer visualizer;
        private IWordFilter wordFilter;
        private IWordProcessor wordProcessor;

        private void Validate()
        {
            A.CallTo(() => fileReader.Read(A<string>.Ignored))
                .MustHaveHappened();
            A.CallTo(() => imageSaver.Save(A<Image>.Ignored, A<string>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored))
                .MustHaveHappened();
            A.CallTo(() => sizeScheme.GetSize(A<FrequentedWord>.Ignored))
                .MustHaveHappened();
            A.CallTo(() => statisticsCollector.GetStatistics(A<IEnumerable<string>>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => visualizer.Visualize(A<IEnumerable<PositionedElement>>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => wordFilter.ToExclude(A<string>.Ignored))
                .MustHaveHappened();
            A.CallTo(() => wordProcessor.Process(A<string>.Ignored))
                .MustHaveHappened();
        }

        private void ConfigureMethods()
        {
            A.CallTo(() => fileReader.Read(A<string>.Ignored))
                .Returns(new[] {"a", "b", "c"});
            A.CallTo(() => wordFilter.ToExclude(A<string>.Ignored))
                .Returns(false);
            A.CallTo(() => wordProcessor.Process(A<string>.Ignored))
                .Returns("s");
            A.CallTo(() => statisticsCollector.GetStatistics(A<IEnumerable<string>>.Ignored))
                .Returns(new[] {new FrequentedWord("s", 1)});
        }

        [Test]
        public void ExecuteNecessaryMethods()
        {
            var app = new Application(
                layouter,
                visualizer,
                fileReader,
                imageSaver,
                statisticsCollector,
                wordFilter,
                sizeScheme,
                wordProcessor);

            ConfigureMethods();

            app.Run("", "");

            Validate();
        }
    }
}