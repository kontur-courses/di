using System.Collections.Generic;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class WordsFromFileReaderTests
{
    private ContainerBuilder builder;
    private string TestFilesDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles");

    [SetUp]
    public void Setup()
    {
        builder = new ContainerBuilder();
        builder.RegisterType<WordsFromFileReader>().AsSelf();
    }

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