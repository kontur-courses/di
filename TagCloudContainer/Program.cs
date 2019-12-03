using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordsSource = "words.txt";
            var outputFile = "wordCloud.bmp";

            var container = PrepareContainer(File.ReadLines(wordsSource));

            var bitmap = container.ResolveNamed<Image>("wordCloud");
            bitmap.Save(outputFile);
        }

        private static IContainer PrepareContainer(IEnumerable<string> wordsSource)
        {
            var builder = new ContainerBuilder();
            builder.Register(c => wordsSource).As<IEnumerable<string>>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<WordCloudLayouter>().As<IWordCloudLayouter>().SingleInstance();
            builder.RegisterType<SqrtStringSizeProvider>().As<IStringSizeProvider>();

            builder.Register(c => new SolidBrush(Color.White)).As<Brush>().Named<Brush>("backgroundBrush")
                .SingleInstance();
            builder.Register(c => new SolidBrush(Color.Indigo)).As<Brush>().Named<Brush>("wordBrush")
                .SingleInstance();
            builder.Register(c => new Pen(Color.Blue)).As<Pen>().SingleInstance();
            builder.Register(c => new Font("ComicSans", 14)).As<Font>().SingleInstance();

            builder.Register(c => new DrawingOptions()).AsSelf().SingleInstance();

            builder.RegisterType<TagCloudVisualizer>().As<IWordVisualizer>();
            
            builder.Register(c => c.Resolve<IWordVisualizer>().CreateImageWithWords(
                c.Resolve<IEnumerable<string>>(),
                c.Resolve<DrawingOptions>())
            ).As<Image>().Named<Image>("wordCloud");
            return builder.Build();
        }
    }
}