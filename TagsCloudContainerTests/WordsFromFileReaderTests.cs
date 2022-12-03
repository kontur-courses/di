using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class WordsFromFileReaderTests
{
    [SetUp]
    public void Setup()
    {
        builder = new ContainerBuilder();
        builder.RegisterType<WordsFromFileReader>().AsSelf();
    }

    private ContainerBuilder builder;
    private readonly string TestFilesDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles");

    [Test]
    public void WordsFromFileReader_ShouldWorkCorrectly()
    {
        builder.RegisterInstance(Path.Combine(TestFilesDir, "DefaultInput.txt")).As<string>();

        using (var scope = builder.Build().BeginLifetimeScope())
        {
            var reader = scope.Resolve<WordsFromFileReader>();
            Approvals.VerifyAll(reader, s => s);
        }
    }

    [Test]
    public void WordsFromFileReader_ShouldWorkCorrectly_WhenEmptyFile()
    {
        builder.RegisterInstance(Path.Combine(TestFilesDir, "EmptyInput.txt")).As<string>();

        using (var scope = builder.Build().BeginLifetimeScope())
        {
            var reader = scope.Resolve<WordsFromFileReader>();
            Approvals.VerifyAll(reader, s => s);
        }
    }
}