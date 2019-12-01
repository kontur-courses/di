using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using Autofac;
using Autofac.Core;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Palettes;
using TagsCloudContainer.Visualizers;
using TagsCloudContainer.Readers;
using TagsCloudContainer.WordPreprocessors;

namespace TagsCloudContainer
{
    [TestFixture]
    class TagCloudGeneratorTests
    {
        [Test]
        public void CreateWithContainer_NotThrow()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point());
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();
            containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                .WithParameters(
                new Parameter[]
                {
                    new NamedParameter("font", new Font("Arial", 20)),
                    new NamedParameter("brush", Brushes.Red)
                }
                );
            containerBuilder.RegisterType<SimpleVsualizer>().As<IVisualizer>().WithParameter("imageSettings", new ImageSettings(640, 640)); ;
            containerBuilder.RegisterType<SimpleReader>().As<IReader>().WithParameter("path", "path");
            containerBuilder.RegisterType<TagsCloudGenerator>().As<TagsCloudGenerator>();

            var container = containerBuilder.Build();
            var tagsCloudGenerator = container.Resolve<TagsCloudGenerator>();
        }

        [Test]
        public void CreateWithContainer_CreateTagCloud()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point());
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();
            containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                .WithParameters(
                new Parameter[]
                {
                    new NamedParameter("font", new Font("Arial", 20)),
                    new NamedParameter("brush", Brushes.Red)
                }
                );
            containerBuilder.RegisterType<SimpleVsualizer>().As<IVisualizer>().WithParameter("imageSettings", new ImageSettings(40, 640)); ;
            containerBuilder.RegisterType<SimpleReader>().As<IReader>().WithParameter("path", @"E:\Projects\Shpora1\di\TagsCloudContainer\Words.txt");
            containerBuilder.RegisterType<TagsCloudGenerator>().As<TagsCloudGenerator>();

            var container = containerBuilder.Build();
            var tagsCloudGenerator = container.Resolve<TagsCloudGenerator>();

            var bitmap = tagsCloudGenerator.CreateTagCloud();
            bitmap.Save("image.png");
        }
    }
}
