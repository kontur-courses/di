using System.Collections.Generic;
using System.Drawing;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class DefaultRectangleDistributorTests
{
    private DefaultRectanglesDistributor rectangleDistributor;

    [Test]
    public void Should_DistributeEqually_WhenEqualFrequency()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordsHandler>().Setup(h => h.WordDistribution).Returns(
                new Dictionary<string, int>
                {
                    {"Abc", 1},
                    {"AAA", 1},
                    {"DAb", 1}
                });
            mock.Mock<ISettingsProvider>().Setup(h => h.Settings).Returns(
                new Settings
                {
                    Font = new Font(FontFamily.GenericMonospace, 20),
                    FrequencyRatio = 2f
                });
            var wordsHandler = mock.Create<IWordsHandler>();
            var settings = mock.Create<ISettingsProvider>();
            rectangleDistributor =
                new DefaultRectanglesDistributor(wordsHandler, settings, new SpiralCloudLayouter(Point.Empty));
        }

        Approvals.VerifyAll(rectangleDistributor.DistributedRectangles);
    }

    [Test]
    public void Should_IncreaseRectanglesSize_WhenWordIsRepeated()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordsHandler>().Setup(h => h.WordDistribution).Returns(
                new Dictionary<string, int>
                {
                    {"Abc", 1},
                    {"AAA", 2},
                    {"DAb", 3}
                });
            mock.Mock<ISettingsProvider>().Setup(h => h.Settings).Returns(
                new Settings
                {
                    Font = new Font(FontFamily.GenericMonospace, 20),
                    FrequencyRatio = 2f
                });
            var wordsHandler = mock.Create<IWordsHandler>();
            var settings = mock.Create<ISettingsProvider>();
            rectangleDistributor =
                new DefaultRectanglesDistributor(wordsHandler, settings, new SpiralCloudLayouter(Point.Empty));
        }

        Approvals.VerifyAll(rectangleDistributor.DistributedRectangles);
    }
}