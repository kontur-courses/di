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
            containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();
            containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                .WithParameters(
                new Parameter[]
                {
                    new NamedParameter("font", new Font("Arial", 20)),
                    new NamedParameter("brush", Brushes.Red)
                }
                );
            containerBuilder.RegisterType<SimpleVsualizer>().As<IVisualizer>();
            containerBuilder.RegisterType<TagsCloudGenerator>().As<TagsCloudGenerator>();

            var container = containerBuilder.Build();
            var tagsCloudGenerator = container.Resolve<TagsCloudGenerator>();
        }
    }
}
