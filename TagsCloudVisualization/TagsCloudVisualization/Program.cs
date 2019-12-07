using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    internal class Program
    {
        internal static void Main()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var fullPath = workingDirectory + "\\cloud.bmp";
            var textDirectory = Environment.CurrentDirectory + "\\HarryPotter.txt";
            var container = PrepareContainer(new Point(0, 0), textDirectory, 100);
            container.Resolve<Bitmap>().Save(fullPath);
            Console.WriteLine("rectangle saved to: " + fullPath);
        }

        //1. Need to write out badWordsList and reed it from resources folder also the same with HarryPotter book
        //2. Need to Fix Encoding
        //3. Need to add more functions
        private static IContainer PrepareContainer(Point point, string path, int maxCountWords)
        {
            var builder = new ContainerBuilder();

            builder.Register(c => TextFromPathReader.FindTextLines(path))
                .As<IEnumerable<string>>().Named<IEnumerable<string>>("lineSource");
            builder.RegisterType<TextParser>().AsSelf();

            builder.Register(c => c.Resolve<TextParser>().wordSource).Named<IEnumerable<string>>("wordsSource");

            builder.Register(c => new Font("ComicSans", 10)).As<Font>().SingleInstance();
            builder.Register(c => new SolidBrush(Color.Red)).As<SolidBrush>().SingleInstance();
            builder.Register(c => Color.Aqua).As<Color>().SingleInstance();
            builder.RegisterType<DrawerOption>().As<IDrawerOption>();

            builder.Register(c => point).As<Point>();
            builder.RegisterType<FermaSpiral>().As<ISpiral>().SingleInstance();

            builder.RegisterType<FrequencySelector>().As<IFrequencyObjectSelector<string>>().SingleInstance();
            builder.RegisterType<FrequencyCounter<string>>().AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<FrequencyCounter<string>>()
                    .GetFrequencyDictionary(c.ResolveNamed<IEnumerable<string>>("wordsSource")))
                .As<Dictionary<string, int>>().Named<Dictionary<string, int>>("frequencySource").SingleInstance();

            builder.RegisterType<WordFrequencySizeSelector>().As<ISizableSelector<string, int>>();
            builder.RegisterType<WordsToSizableProvider>().As<ISizableProvider<string>>().SingleInstance();

            builder.Register(c => c.Resolve<ISizableProvider<string>>()
                    .GetSizableObjects(c.ResolveNamed<Dictionary<string, int>>("frequencySource"), maxCountWords))
                .As<IEnumerable<Sizable<string>>>().Named<IEnumerable<Sizable<string>>>("sizableSource");

            builder.RegisterType<CircularDrawableCloudLayouter>().As<IDrawableProvider<string>>();

            builder.RegisterType<TagDrawer>().As<IDrawer>();
            builder.Register(c => c.Resolve<IDrawer>().DrawImage()).As<Bitmap>();

            return builder.Build();
        }
    }
}