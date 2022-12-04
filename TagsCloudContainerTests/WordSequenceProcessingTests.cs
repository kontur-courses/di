using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class WordSequenceProcessingTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WordDistribution_Should_Be_Correct()
    {
        var sequence = new List<string>(
            "AAa aAa B b AA a B b D"
                .Split());

        var wordsHandler = new DefaultWordsHandler(sequence);
        Approvals.VerifyAll(wordsHandler.WordDistribution);
    }


    [Test]
    public void WordDistribution_Should_Be_Correct_WhenEmptyString()
    {
        var sequence = new List<string>();

        var wordsHandler = new DefaultWordsHandler(sequence);
        Approvals.VerifyAll(wordsHandler.WordDistribution);
    }
}