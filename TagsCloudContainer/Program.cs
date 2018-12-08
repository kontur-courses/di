using System.Drawing;
using System.IO;
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

            // Блок использовался для подготовки входных данных из обычного текста
//            var path = Assembly.GetExecutingAssembly().Location;
//            var directory = Path.GetDirectoryName(path);
//            var l = TextFileHelper.ReadFromFile(Path.Combine(directory, "TextSamples", "1984.txt"));
// 
//            TextFileHelper.Rebuildtext(l);

            using (var scope = Container.BeginLifetimeScope())
            {
                var textReader = scope.Resolve<ISourceTextReader>();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "1984_lines.txt");

                var lines = textReader.ReadText(filePath);

                var writer = scope.Resolve<IWordsPreprocessor>();

                var boringWords = new[] {"который", "большой"};

                var preprocessedWords = writer.PreprocessWords(lines, boringWords);

                // TODO: задавать через аргументы коммандной строки
                var size = new Size(1500, 1500);
                var fontFamily = new FontFamily("Times New Roman");
                var brush = Brushes.Black;

                var centerPoint = new Point(size.Width / 2, size.Height / 2);

                var algorithm = scope.Resolve<IAlgorithm>(new TypedParameter(typeof(Point), centerPoint));

                var rectangles = algorithm.GenerateRectanglesSet(preprocessedWords.Take(100));

                var drawer = scope.Resolve<IResultFormatter>();
                drawer.GenerateResult(size, fontFamily, brush, "tag-cloud.png", rectangles);
            }
        }
    }
}
