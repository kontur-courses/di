using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class WordsHandlerWithFilterTests
{
    private WordsHandlerWithFilter WordsHandler;
    private List<string> TestList;
    private List<string> TestFilter;

    [Test]
    public void Should_WorkAsDefaultHandler_WhenNoFilter()
    {
        TestList = new List<string>(new[]
        {
            "a",
            "b",
            "C",
            "c"
        });
        TestFilter = new List<string>();
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            mock.Mock<IWordFilterProvider>().Setup(s => s.WordFilter).Returns(
                TestFilter);
            var words = mock.Create<IWordSequenceProvider>();
            var filter = mock.Create<IWordFilterProvider>();
            WordsHandler = new WordsHandlerWithFilter(words, filter);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }

    [Test]
    public void Should_Filter_WhenWordIsInFilter()
    {
        TestList = new List<string>(new[]
        {
            "a",
            "b",
            "c",
            "c"
        });
        TestFilter = new List<string>(new[]
        {
            "b",
            "C",
            "b"
        });
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            mock.Mock<IWordFilterProvider>().Setup(s => s.WordFilter).Returns(
                TestFilter);
            var words = mock.Create<IWordSequenceProvider>();
            var filter = mock.Create<IWordFilterProvider>();
            WordsHandler = new WordsHandlerWithFilter(words, filter);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }

    [Test]
    public void Should_NotFilter_WhenFilterContainsWordNotFromWordSequence()
    {
        TestList = new List<string>(new[]
        {
            "a",
            "b",
            "c",
            "c"
        });
        TestFilter = new List<string>(new[]
        {
            "ASD",
            "Csd",
            "bssss"
        });
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            mock.Mock<IWordFilterProvider>().Setup(s => s.WordFilter).Returns(
                TestFilter);
            var words = mock.Create<IWordSequenceProvider>();
            var filter = mock.Create<IWordFilterProvider>();
            WordsHandler = new WordsHandlerWithFilter(words, filter);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }
}