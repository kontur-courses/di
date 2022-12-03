using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
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
        builder = new ContainerBuilder();
        builder.RegisterType<StringSpaceSplitter>().As<IEnumerable<string>>();
        builder.RegisterType<DefaultWordsHandler>().As<IWordsHandler>();
    }

    private ContainerBuilder builder;

    [Test]
    public void WordDistribution_Should_Be_Correct()
    {
        builder.RegisterInstance("A B D a a b A").As<string>();
        using (var scope = builder.Build().BeginLifetimeScope())
        {
            var wordsHadler = scope.Resolve<IWordsHandler>();
            Approvals.VerifyAll(wordsHadler.WordDistribution);
        }
    }

    [Test]
    public void WordDistribution_Should_Be_Correct_WhenEmptyString()
    {
        builder.RegisterInstance("").As<string>();

        using (var scope = builder.Build().BeginLifetimeScope())
        {
            var wordsHadler = scope.Resolve<IWordsHandler>();
            Approvals.VerifyAll(wordsHadler.WordDistribution);
        }
    }
}