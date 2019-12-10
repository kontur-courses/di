using NUnit.Framework;
using System.Drawing;
using Autofac;
using Autofac.Core;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Palettes;
using TagsCloudContainer.Visualizers;
using TagsCloudContainer.Readers;
using TagsCloudContainer.WordPreprocessors;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.TokensAndSettings;
using System;
using System.IO;
using TagsCloudContainer.PaintersWords;
using TagsCloudContainer.TagsCloudGenerators;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class TagCloudGeneratorTests
    {
        private ContainerBuilder containerBuilder;

        [SetUp]
        public void SetUp()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point());
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<SimpleWordFilter>().As<IWordFilter>();
            containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();
            containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                .WithParameters(
                new Parameter[]
                {
                    new NamedParameter("font", new Font("Arial", 20)),
                    new NamedParameter("painterWords", new SimplePainterWords(Brushes.Red))
                });
            containerBuilder.RegisterType<SimpleVisualizer>().As<IVisualizer>().WithParameter("imageSettings", new ImageSettings(40, 640)); ;
            containerBuilder.RegisterType<SimpleReader>().As<IReader>().WithParameter("path", Path.Combine(Environment.CurrentDirectory, @"TagsCloudContainer\WordsRus.txt"));
            containerBuilder.RegisterType<TagsCloudGenerator>().As<TagsCloudGenerator>();
        }

        [Test]
        public void CreateWithContainer_CreateTagCloud()
        {
            var container = containerBuilder.Build();
            var tagsCloudGenerator = container.Resolve<TagsCloudGenerator>();

            var bitmap = tagsCloudGenerator.CreateTagCloud();
            bitmap.Save("image.png");
        }
    }
}
