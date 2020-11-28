using System;
using System.Drawing;
using Autofac;
using Autofac.Core;
using TagsCloud.BoringWordsDetectors;
using TagsCloud.CloudRenderers;
using TagsCloud.StatisticProviders;
using TagsCloud.WordLayouters;
using TagsCloud.WordReaders;

namespace TagsCloud
{
    public static class Program
    {
        public static void Main()
        {
            while (true)
            {
                MakeCloud();
            }
        }

        public static void MakeCloud(IContainer container = null)
        {
            container ??= ConfigureContainer();
            
            var words = container.Resolve<IWordReader>().ReadWords();
            if (words == null) return;
            
            var statistic = container.Resolve<IStatisticProvider>().GetWordStatistics(words);
            
            var layouter = container.Resolve<IWordLayouter>();
            layouter.AddWords(statistic);
            
            var path = container.Resolve<ICloudRenderer>().RenderCloud();
            Console.WriteLine($"Cloud saved in {path}");
        }
        
        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            
            Console.Write("File path: ");
            var filePath = @"C:\Users\borov\Desktop\abc.txt";//Console.ReadLine();
            builder.RegisterType<RegexWordReader>()
                .As<IWordReader>()
                .WithParameter("filePath", filePath);

            builder.RegisterType<StatisticProvider>()
                .As<IStatisticProvider>()
                .WithParameter(new TypedParameter(typeof(IBoringWordsDetector), new ByCollectionBoringWordsDetector()));

            var font = new FontFamily("Arial");//ReadFont();
            builder.RegisterType<WordLayouter>()
                .SingleInstance()
                .As<IWordLayouter>()
                .WithParameters(new Parameter[]
                {
                    new TypedParameter(typeof(FontFamily), font),
                    new TypedParameter(typeof(IPointsLayout), new SpiralPoints()),
                });
            
            var width = 4000;//ReadInteger("Image width");
            var height = 2000;//ReadInteger("Image height");
            builder.RegisterType<CloudRenderer>()
                .As<ICloudRenderer>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("width", width),
                    new NamedParameter("height", height), 
                });
            
            return builder.Build();
        }

        private static int ReadInteger(string parameterName)
        {
            while (true)
            {
                Console.Write($"{parameterName}: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result)) return result;
                Console.WriteLine("Incorrect string");
            }
        }
        
        private static FontFamily ReadFont()
        {
            while (true)
            {
                Console.Write("Font: ");
                var input = Console.ReadLine();
                try
                {
                    var font = new FontFamily(input);
                    return font;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"{input} doesn't exist");
                }
            }
        }
    }
}