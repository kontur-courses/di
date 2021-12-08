using Autofac;
using NUnit.Framework;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Tests
{
    public class Tests
    {
        private IContainer container;
        
        [SetUp]
        public void Setup()
        {
            container = ContainerConfig.ConfigureContainer();
        }

        [Test]
        public void Demo()
        {
            var textAnalyzer = container.Resolve<ITextAnalyzer>();
            var statistics = textAnalyzer.GetWordStatistics(@"..\..\..\Resources\TextSamples\test.txt");
        }
    }
}