using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = GetContainerBuilder(args);
            var container = builder.Build();
            DrawTagCloud(container);
        }

        private static void DrawTagCloud(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var cloudLayouter = scope.Resolve<ICloudLayouter>();
                var paintConfig = scope.Resolve<IPaintConfig>();
                var textParser = scope.Resolve<ITextParser>();
                var painter = new CloudPainter(cloudLayouter, paintConfig, textParser);
                var pathToSaving = "../../TagCloud.png";
                painter.Draw(pathToSaving);
            }
        }

        public static ContainerBuilder GetContainerBuilder(string[] args)
        {
            var builder = new ContainerBuilder();
            var center = new Point(1000, 1000);
            var boringWords = new HashSet<string>();
            var path = @"D:\di\Homework\TagsCloudContainer\words.txt";
            var imageSize = new Size(2000, 2000);
            var colors = new List<Brush>()
            {
                Brushes.DeepSkyBlue,
                Brushes.BlueViolet,
                Brushes.LightSkyBlue
            };
            var fontName = "Arial";
            var fontSize = 20;

            builder.Register(pc => new PaintCofig(colors, fontName, fontSize, imageSize))
                .As<IPaintConfig>();
            builder.Register(cl => new CircularCloudLayouter(center)).As<ICloudLayouter>();
            builder.Register(tp => new TextParser(path, boringWords)).As<ITextParser>();

            return builder;
        }
    }
}
