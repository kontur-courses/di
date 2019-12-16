using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Autofac;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly TextHandler fileHandler;
        private readonly ITagCloudBuildingAlgorithm algorithmToBuild;

        public TagCloudBuilder(TextHandler fileHandler,
            ITagCloudBuildingAlgorithm algorithmToBuild)
        {
            this.fileHandler = fileHandler;
            this.algorithmToBuild = algorithmToBuild;
        }

        public IEnumerable<Tag> GetTagsCloud()
        {
            var frequencyDict = fileHandler.GetWordsFrequencyDict();
            return algorithmToBuild.GetTags(frequencyDict);
        }

        [TestFixture]
        public class InjectionTest
        {
            [Test]
            public void TagCloudBuilderInjections()
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(new TextFileReader("test")).As<ITextReader>();
                builder.RegisterInstance(new NothingDullEliminator())
                    .As<IDullWordsEliminator>();
                builder.RegisterType<DefaultAlgorithm>().As<ITagCloudBuildingAlgorithm>();
                builder.RegisterType<TagCloudBuilder>().As<ITagCloudBuilder>();
                builder.RegisterType<DefaultTextHandler>().As<TextHandler>();
                var container = builder.Build();

                using (var scope = container.BeginLifetimeScope())
                {
                    var tagCloudBuilder = scope.Resolve<ITagCloudBuilder>() as TagCloudBuilder;

                    tagCloudBuilder.algorithmToBuild.Should().BeOfType(typeof(DefaultAlgorithm));
                    tagCloudBuilder.fileHandler.Should().BeOfType(typeof(DefaultTextHandler));
                }
            }
        }
    }
}