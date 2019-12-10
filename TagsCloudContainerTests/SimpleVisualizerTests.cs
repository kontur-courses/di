using Autofac;
using Autofac.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintersWords;
using TagsCloudContainer.Palettes;
using TagsCloudContainer.TokensAndSettings;
using TagsCloudContainer.Visualizers;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class SimpleVisualizerTests
    {
        [Test]
        public void VisualizeCloud()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point());
            containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                .WithParameters(
                new Parameter[]
                {
                    new NamedParameter("font", new Font("Arial", 20)),
                    new NamedParameter("painterWords", new SimplePainterWords(Brushes.Red))
                }
                );
            containerBuilder.RegisterType<SimpleVisualizer>().As<IVisualizer>().WithParameter("imageSettings", new ImageSettings(640, 640));

            var conteiner = containerBuilder.Build();

            var wordTokens = new List<WordToken>()
            {
                new WordToken("Aaa", 1),
                new WordToken("BbbbB", 1),
                new WordToken("g", 2)
            };

            var visualizer = conteiner.Resolve<IVisualizer>();

            var result = visualizer.VisualizeCloud(wordTokens);

            result.Save("image.png");
        }
    }
}
