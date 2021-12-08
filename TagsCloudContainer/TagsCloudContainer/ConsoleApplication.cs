using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TextPreparation;

namespace TagsCloudContainer
{
    public static class ConsoleApplication
    {
        private static int id = 1;

        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new TxtFileReader()).As<IFileReader>();
            builder.RegisterInstance(new DefaultWordHelper()).As<IWordsHelper>();
            builder.Register(_ => "Print path to file with words").Named<string>("inputFileMessage");
            builder.Register(_ => "Print colors of tags separated by whitespace").Named<string>("tagColorsMessage");
            builder.Register(_ => "Print background color").Named<string>("backgroundColorMessage");
            builder.Register(_ => new Size(30, 30)).Named<Size>("minTagSize");
            builder.Register(_ => new Size(200, 50)).Named<Size>("maxTagSize");
            builder.Register(_ => "Print coordinates of center of your image separated by whitespace")
                .Named<string>("coordinatesMessage");
            builder.Register(_ => "If you want to use default points generator, print 'y'")
                .Named<string>("useDefaultGeneratorMessage");
            builder.Register(_ => "y").Named<string>("positiveAnswer");
            builder.Register(_ => "Print start spiral radius").Named<string>("radiusMessage");
            builder.Register(_ => "Print start angle").Named<string>("angleMessage");
            builder.Register(_ => "Print angle delta").Named<string>("angleDeltaMessage");
            builder.Register(_ => "Print radius delta").Named<string>("radiusDeltaMessage");
            builder.Register(_ => 0.8).Named<double>("reductionCoefficient");
            builder.Register(_ => 5f).Named<float>("minFontSize");
            builder.Register(_ => FontFamily.GenericSansSerif).Named<FontFamily>("fontFamily");
            builder.Register(_ => Brushes.Azure).Named<Brush>("textBrush");
            builder.Register(_ => "Result saved to Samples/").Named<string>("finalMessage");
            var container = builder.Build();
            while (true)
            {
                using var scope = container.BeginLifetimeScope();
                Console.WriteLine(scope.ResolveNamed<string>("inputFileMessage"));
                var path = Console.ReadLine();
                var wordsToVisualize = scope.Resolve<IWordsHelper>()
                    .GetAllWordsToVisualize(scope.Resolve<IFileReader>().GetAllWords(path));
                Console.WriteLine(scope.ResolveNamed<string>("tagColorsMessage"));
                var colors = Console.ReadLine()?.Split(' ').Select(Color.FromName).ToList();
                Console.WriteLine(scope.ResolveNamed<string>("backgroundColorMessage"));
                var backgroundColor = Color.FromName(Console.ReadLine() ?? string.Empty);
                var minTagSize = scope.ResolveNamed<Size>("minTagSize");
                var maxTagSize = scope.ResolveNamed<Size>("maxTagSize");
                Console.WriteLine("Print coordinates of center of your image separated by whitespace");
                var coordinates = Console.ReadLine()?.Split(' ').Select(int.Parse).ToList();
                if (coordinates == null || coordinates.Count() != 2)
                {
                    throw new ArgumentException();
                }

                var center = new Point(coordinates[0], coordinates[1]);
                Console.WriteLine(scope.ResolveNamed<string>("useDefaultGeneratorMessage"));
                var pointsGenerator = new SpiralPointsGenerator(center);
                var answer = Console.ReadLine();
                var positiveResult = scope.ResolveNamed<string>("positiveAnswer");
                if (answer == null || !answer.Equals(positiveResult))
                {
                    Console.WriteLine(scope.ResolveNamed<string>("radiusMessage"));
                    var startRadius = double.Parse(Console.ReadLine() ?? string.Empty);
                    Console.WriteLine(scope.ResolveNamed<string>("angleMessage"));
                    var startAngle = double.Parse(Console.ReadLine() ?? string.Empty);
                    Console.WriteLine(scope.ResolveNamed<string>("angleDeltaMessage"));
                    var angleDelta = double.Parse(Console.ReadLine() ?? string.Empty);
                    Console.WriteLine(scope.ResolveNamed<string>("radiusDeltaMessage"));
                    var radiusDelta = double.Parse(Console.ReadLine() ?? string.Empty);
                    pointsGenerator =
                        new SpiralPointsGenerator(center, startRadius, startAngle, angleDelta, radiusDelta);
                }

                var reductionCoefficient = scope.ResolveNamed<double>("reductionCoefficient");
                var minFontSize = scope.ResolveNamed<float>("minFontSize");
                var fontFamily = scope.ResolveNamed<FontFamily>("fontFamily");
                var brush = scope.ResolveNamed<Brush>("textBrush");
                var fileName = id + ".png";
                Visualizer.GetCloudVisualization(wordsToVisualize, colors, backgroundColor, minTagSize, maxTagSize,
                        new CircularCloudLayouter(pointsGenerator), reductionCoefficient, minFontSize, fontFamily,
                        brush)
                    .Save("../../Samples/" + fileName, ImageFormat.Png);
                Console.WriteLine(scope.ResolveNamed<string>("finalMessage") + fileName);
            }
        }
    }
}