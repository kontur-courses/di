using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.Clients;
using TagsCloudContainer.ResultFormatters;
using TagsCloudContainer.SourceTextReaders;
using TagsCloudContainer.TextPreprocessors;

namespace TagsCloudContainer
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<TxtSourceTextReader>().As<ISourceTextReader>();
            builder.RegisterType<BasicTextPreprocessor>().As<ITextPreprocessor>();
            builder.RegisterType<BasicResultFormatter>().As<IResultFormatter>();
            builder.RegisterType<BasicAlgorithm>().As<IAlgorithm>();
            builder.RegisterType<ConsoleClient>().As<IClient>();

            Container = builder.Build();

            for (int i = 0; i < 3; i++)
            {
                var layouter = new CircularCloudLayouter(new Point(1000, 1000));
                var rectangles = CircularCloudLayouterGenerator.GenerateRectanglesSet(layouter, 50, 100, 150, 50, 75);
                CircularCloudLayouterDrawer.DrawRectanglesSet(layouter.Size, $"tag-cloud-{i + 1}.png", rectangles);
            }
        }
    }
}
