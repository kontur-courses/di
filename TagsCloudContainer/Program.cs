using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using NHunspell;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.Clients;
using TagsCloudContainer.Helpers;
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
            builder.RegisterType<BasicWordsPreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterType<CircularCloudLayouter>().As<IAlgorithm>();
            builder.RegisterType<CircularCloudLayouterDrawer>().As<IResultFormatter>();

            builder.RegisterType<ConsoleClient>().As<IClient>();

            Container = builder.Build();

//            var path = Assembly.GetExecutingAssembly().Location;
//            var directory = Path.GetDirectoryName(path);
//            var l = TextFileHelper.ReadFromFile(Path.Combine(directory, "TextSamples",
//                "1984.txt"));
//
//            TextFileHelper.Rebuildtext(l);


            using (var scope = Container.BeginLifetimeScope())
            {
                var x = scope.Resolve<ISourceTextReader>();

                var assembly = Assembly.GetExecutingAssembly();

                var resourceName = "_1984_lines";

//                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
//                using (StreamReader reader = new StreamReader(stream))
//                {
//                    string result = reader.ReadLine();
//                }
                var lines = x.ReadText(resourceName);
//
                var writer = scope.Resolve<IWordsPreprocessor>();
                var preprocessWords = writer.PreprocessWords(lines);


                var res = new Dictionary<string, int>();

                foreach (var word in preprocessWords)
                {
                    res.TryGetValue(word, out var count);
                    res[word] = count + 1;
                }

                var pairs = res.OrderByDescending(e => e.Value);
//                var layouter = new CircularCloudLayouter(new Point(5000, 5000));

                var centerPoint = new Point(5000, 5000);

                var layouter = scope.Resolve<IAlgorithm>(
                    new TypedParameter(typeof(Point), centerPoint));

                var rectangles = layouter.GenerateRectanglesSet(pairs.Take(50), 100, 150, 50, 75);

                var size = new Size(10000, 10000);
                var font = new Font("Times New Roman", 12);
                var brush = Brushes.Red;

                var drawer = scope.Resolve<IResultFormatter>();
                drawer.GenerateResult(size, font, brush, $"tag-cloud.png", rectangles);

            }

        }
    }
}
