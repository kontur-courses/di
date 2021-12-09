using System;
using System.IO;
using Autofac;
using NUnit.Framework;
using TagsCloud.Visualization;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.LayouterCores;

namespace TagsCloud.FunctionalTests
{
    public class VisualizerTests
    {
        [TestCase("txt", TestName = "txt")]
        [TestCase("doc", TestName = "doc")]
        [TestCase("docx", TestName = "docx")]
        [TestCase("pdf", TestName = "pdf")]
        public void Should_ReadWords_From(string extension)
        {
            var settings = GenerateDefaultSettings(extension);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            var container = builder.Build();

            var visualizer = container.Resolve<ILayouterCore>();
            visualizer.GenerateImage().Dispose();
        }

        private TagsCloudModuleSettings GenerateDefaultSettings(string extension) =>
            new()
            {
                InputWordsFile = Path.Combine(Directory.GetCurrentDirectory(), $"test.{extension}"),
                LayouterType = typeof(CircularCloudLayouter),
                LayoutVisitor = new RandomColorDrawerVisitor()
            };
    }
}