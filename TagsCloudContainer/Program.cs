using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Autofac;
using Autofac.Core;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.DataProviders;
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

            var size = new Size(1500, 1500);
            var fontFamily = new FontFamily("Times New Roman");
            var brush = Brushes.Black;
            var centerPoint = new Point(size.Width / 2, size.Height / 2);
            var boringWords = new[] { "который", "большой" };
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "1984_lines.txt");


            var builder = new ContainerBuilder();

            builder.Register(e => new TxtSourceTextReader(filePath)).As<ISourceTextReader>();
            builder.Register(e => new BasicWordsPreprocessor(boringWords)).As<IWordsPreprocessor>();
            builder.Register(e => new ArchimedeanSpiral(centerPoint)).As<ISpiral>();
            builder.RegisterType<CircularCloudAlgorithm>()
                .As<IAlgorithm>()
                .WithParameter(new TypedParameter(typeof(Point), centerPoint))
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(ISpiral) && pi.Name == "spiral",
                    (pi, ctx) => ctx.Resolve<ISpiral>()));
            builder.RegisterType<CircularCloudLayouterResultFormatter>().As<IResultFormatter>();
            builder.RegisterType<DataProvider>().As<IDataProvider>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var drawer = scope.Resolve<IResultFormatter>();
                drawer.GenerateResult(size, fontFamily, brush, "tag-cloud.png");
            }
        }
    }
}
