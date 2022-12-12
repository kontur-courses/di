using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class DefaltWordsHandlerTests
{
    private DefaultWordsHandler WordsHandler;
    private List<string> TestList;

    [Test]
    public void Should_Register_WhenNewWord()
    {
        TestList = new List<string>(new[]
        {
            "a",
            "b"
        });
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            var mocks = mock.Create<IWordSequenceProvider>();
            WordsHandler = new DefaultWordsHandler(mocks);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }

    [Test]
    public void Should_IncreaseFrequency_WhenWordIsRepeated()
    {
        TestList = new List<string>(new[]
        {
            "a",
            "a",
            "a"
        });
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            var mocks = mock.Create<IWordSequenceProvider>();
            WordsHandler = new DefaultWordsHandler(mocks);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }

    [Test]
    public void Should_SetWordToLower()
    {
        TestList = new List<string>(new[]
        {
            "A",
            "a"
        });
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            var mocks = mock.Create<IWordSequenceProvider>();
            WordsHandler = new DefaultWordsHandler(mocks);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }

    [Test]
    public void Should_BeEmpty_WhenNoWords()
    {
        TestList = new List<string>();
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IWordSequenceProvider>().Setup(s => s.WordSequence).Returns(
                TestList);
            var mocks = mock.Create<IWordSequenceProvider>();
            WordsHandler = new DefaultWordsHandler(mocks);
        }

        Approvals.VerifyAll(WordsHandler.WordDistribution);
    }
}