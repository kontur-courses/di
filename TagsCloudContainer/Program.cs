using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            builder.RegisterType<BasicWordsPreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterType<CircularCloudAlgorithm>().As<IAlgorithm>();
            builder.RegisterType<CircularCloudLayouterResultFormatter>().As<IResultFormatter>();

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
                var textReader = scope.Resolve<ISourceTextReader>();

                var resourceName = "1984_lines.txt";

                var lines = textReader.ReadText(resourceName);

                var writer = scope.Resolve<IWordsPreprocessor>();
                var preprocessWords = writer.PreprocessWords(lines);


                var res = new Dictionary<string, int>();

                foreach (var word in preprocessWords)
                {
                    res.TryGetValue(word, out var count);
                    res[word] = count + 1;
                }

                var pairs = res.OrderByDescending(e => e.Value);


                // TODO: задавать через аргументы коммандной строки
                var size = new Size(10000, 10000);
                var font = new Font("Times New Roman", 10);
                var brush = Brushes.Black;
                var centerPoint = new Point(5000, 5000);

                var algorithm = scope.Resolve<IAlgorithm>(new TypedParameter(typeof(Point), centerPoint));

                var rectangles = algorithm.GenerateRectanglesSet(pairs.Take(50));

                var drawer = scope.Resolve<IResultFormatter>();
                drawer.GenerateResult(size, font, brush, $"tag-cloud.png", rectangles);

            }

        }
    }
}
