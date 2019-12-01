using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;

namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordsSource = "words.txt";
            var outputFile = "wordCloud.bmp";
            var builder = new ContainerBuilder();
            builder.Register(c => File.ReadLines(wordsSource)).As<IEnumerable<string>>();
            builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>().SingleInstance();
            builder.RegisterType<WordCloudLayouter>().As<IWordCloudLayouter>().SingleInstance();
            builder.Register(c => new SolidBrush(Color.White)).As<Brush>().Named("backgroundBrush", typeof(Brush))
                .SingleInstance();
            builder.Register(c => new SolidBrush(Color.DarkOrange)).As<Brush>().Named("wordBrush", typeof(Brush))
                .SingleInstance();

            builder.Register(c => new Font("ComicSans", 14)).As<Font>().SingleInstance();
            builder.Register(c => LayouterVisualizer.CreateCloudWithWordsFromFile(c.Resolve<IEnumerable<string>>(),
                c.Resolve<ICircularCloudLayouter>(),
                c.Resolve<IWordCloudLayouter>(),
                c.ResolveNamed<Brush>("backgroundBrush"),
                c.ResolveNamed<Brush>("wordBrush"),
                c.Resolve<Font>())
            ).As<TagCloudBitmapContainer>();
            var container = builder.Build();

            var bitmap = container.Resolve<TagCloudBitmapContainer>();
            bitmap.Save(outputFile);
        }
    }
}