using System;
using System.Collections.Generic;
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
        public static void Run()
        {
            var container = BuildContainer();
            using var scope = container.BeginLifetimeScope();
            while (true)
            {
                var rnd = new Random();
                var parametersToVisualize = GetParametersToVisualize(scope);
                var bitmap = Visualizer.GetCloudVisualization(parametersToVisualize.wordsToVisualize,
                    parametersToVisualize.colors,
                    parametersToVisualize.backgroundColor, parametersToVisualize.minTagSize,
                    parametersToVisualize.maxTagSize, parametersToVisualize.layouter,
                    parametersToVisualize.reductionCoefficient, parametersToVisualize.minFontSize,
                    parametersToVisualize.fontFamily, parametersToVisualize.brush);
                var fileName = rnd.Next().ToString();
                FileSaver.Save(bitmap, fileName, "../../Samples/", parametersToVisualize.format);
                
                Console.WriteLine(scope.ResolveNamed<string>("finalMessage") + fileName + "." + parametersToVisualize.format);
            }
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new TxtFileReader(new WordsReader())).Named<TxtFileReader>("txtReader");
            builder.Register(_ => new DocFileReader(new WordsReader())).Named<DocFileReader>("docReader");
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
            builder.Register(_ => "Print format to save").Named<string>("formatMessage");
            builder.Register(_ => "Result saved to Samples/").Named<string>("finalMessage");
            return builder.Build();
        }

        private static (List<string> wordsToVisualize, List<Color> colors, Color backgroundColor, Size minTagSize, Size
            maxTagSize, CircularCloudLayouter layouter, double reductionCoefficient, float minFontSize, FontFamily
            fontFamily, Brush brush, string format) GetParametersToVisualize(ILifetimeScope scope)
        {
            var path = AskUserForPath(scope);
            var wordsToVisualize = scope.Resolve<IWordsHelper>()
                .GetAllWordsToVisualize(GetFileReaderByFilePath(scope, path).GetAllWords(path));
            var colors = AskUserForTagColors(scope);
            var backgroundColor = AskUserForBackgroundColor(scope);
            var minTagSize = scope.ResolveNamed<Size>("minTagSize");
            var maxTagSize = scope.ResolveNamed<Size>("maxTagSize");
            var center = AskUserForCenterOfImage(scope);
            var pointsGenerator = AskUserForUsingDefaultGenerator(scope)
                ? AskUserForCustomPointsGenerator(scope, center)
                : new SpiralPointsGenerator(center);
            var reductionCoefficient = scope.ResolveNamed<double>("reductionCoefficient");
            var minFontSize = scope.ResolveNamed<float>("minFontSize");
            var fontFamily = scope.ResolveNamed<FontFamily>("fontFamily");
            var brush = scope.ResolveNamed<Brush>("textBrush");
            var format = AskUserForSavingFormat(scope);
            return (wordsToVisualize, colors, backgroundColor, minTagSize, maxTagSize,
                new CircularCloudLayouter(pointsGenerator), reductionCoefficient, minFontSize, fontFamily, brush, format);
        }

        private static string AskUserForPath(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("inputFileMessage"));
            return Console.ReadLine();
        }

        private static List<Color> AskUserForTagColors(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("tagColorsMessage"));
            return Console.ReadLine()?.Split(' ').Select(Color.FromName).ToList();
        }

        private static Color AskUserForBackgroundColor(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("backgroundColorMessage"));
            return Color.FromName(Console.ReadLine() ?? string.Empty);
        }

        private static Point AskUserForCenterOfImage(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("coordinatesMessage"));
            var coordinates = Console.ReadLine()?.Split(' ').Select(int.Parse).ToList();
            if (coordinates == null || coordinates.Count() != 2)
            {
                throw new ArgumentException();
            }

            return new Point(coordinates[0], coordinates[1]);
        }

        private static bool AskUserForUsingDefaultGenerator(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("useDefaultGeneratorMessage"));
            var answer = Console.ReadLine();
            return answer != null && !answer.Equals(scope.ResolveNamed<string>("positiveAnswer"));
        }
        
        private static string AskUserForSavingFormat(ILifetimeScope scope)
        {
            Console.WriteLine(scope.ResolveNamed<string>("formatMessage"));
            return Console.ReadLine();
        }

        private static SpiralPointsGenerator AskUserForCustomPointsGenerator(ILifetimeScope scope, Point center)
        {
            Console.WriteLine(scope.ResolveNamed<string>("radiusMessage"));
            var startRadius = double.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine(scope.ResolveNamed<string>("angleMessage"));
            var startAngle = double.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine(scope.ResolveNamed<string>("angleDeltaMessage"));
            var angleDelta = double.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine(scope.ResolveNamed<string>("radiusDeltaMessage"));
            var radiusDelta = double.Parse(Console.ReadLine() ?? string.Empty);
            return new SpiralPointsGenerator(center, startRadius, startAngle, angleDelta, radiusDelta);
        }

        private static IFileReader GetFileReaderByFilePath(ILifetimeScope scope, string path)
        {
            if (path is not {Length: >= 3})
            {
                throw new ArgumentException("File path is Invalid");
            }

            if (path.Substring(path.Length - 3).Equals("txt"))
            {
                return scope.ResolveNamed<TxtFileReader>("txtReader");
            }
            else if (path.Length >= 4 && path.Substring(path.Length - 4).Equals("docx"))
            {
                return scope.ResolveNamed<DocFileReader>("docReader");    
            }

            throw new ArgumentException("Invalid file extension");
        }
    }
}