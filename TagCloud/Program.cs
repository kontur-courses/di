using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.FileReader;
using TagCloud.WordConverter;
using TagCloud.WordFilter;
using TagCloud.FrequencyAnalyzer;
using TagCloud.WordColoring;
using TagCloud.ImageProcessing;
using TagCloud.CloudLayouter;
using TagCloud.TextParsing;
using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;

namespace TagCloud
{
    public class Program
    {
        private static IContainer container;

        static IContainer ConfigureContainer(IAppConfig appConfig)
        {
            var builder = new ContainerBuilder();

            //получение текста и списка слов
            builder.RegisterType<TxtFileReader>().As<IFileReader>();

            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.RegisterType<ToInitialFormConverter>().As<IWordConverter>();
            builder.RegisterType<ToLowerConverter>().As<IWordConverter>();
            builder.RegisterType<ConvertersExecutor>().As<IConvertersExecutor>();

            builder.RegisterType<BoringWordFilter>().As<IWordFilter>();
            builder.RegisterType<FiltersExecutor>().As<IFiltersExecutor>();

            builder.RegisterType<WordsFrequencyAnalyzer>().As<IWordsFrequencyAnalyzer>();

            //получение раскладчика
            builder.Register(c => new CircularCloudLayouter(new Point(0, 0))).As<ICloudLayouter>(); //переделать конструктор для доп получения IPointGenerator
            // builder.RegisterType<ArchimedeanSpiral>.As<IPointGenerator>() 
            // сделать наследование от IPointGenerator

            //получение настроек изображения
            builder.Register(с => appConfig.imageSettings).As<IImageSettings>(); 
            builder.Register(с => appConfig.imageSettings.WordColoring).As<IWordColoring>();  // передать макс и мин значения в сдучае GradientColoring
            builder.Register(с => appConfig.imageSettings.FontFamily).As<FontFamily>();

            //получение генератор изображения
            builder.RegisterType<CloudImageGenerator>().AsSelf(); // сделать интерфейс ICloudImageGenerator // мб возьмёт не тот конструктор, указать нужный конструктоор

            //получение приложения
            builder.RegisterType<ConsoleApp>().As<IApp>();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var appConfig = new AppConfigProvider(args).GetAppConfig();

            container = ConfigureContainer(appConfig);

            var app = container.Resolve<IApp>();

            app.Run(appConfig);
        }
        private static void CreateTestText()
        {
            var lines = File.ReadAllLines("TestWords.txt");

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                var chars = tokens[0].ToCharArray();

                chars[0] = char.ToUpper(chars[0]);

                var word = new string(chars);

                var frequency = Convert.ToInt32(Math.Round(double.Parse(tokens[1]) / 100));

                dict.Add(word, frequency);
            }

            var sb = new StringBuilder();

            foreach (var pair in dict)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    sb.AppendLine(pair.Key);
                }
            }

            File.WriteAllText("TestText.txt", sb.ToString());
        }
    }
}

