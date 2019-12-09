using Autofac;
using TagsCloud.FileReader;
using TagsCloud.Interfaces;
using TagsCloud.CloudLayouter;
using TagsCloud.WordProcessing;
using TagsCloud.FinalProcessing;
using CommandLine;
using System.Drawing;
using System.Reflection;
using System;
using System.IO;

namespace TagsCloud
{
    class Program
    {

        private static IContainer Container { get; set; }
        class Options
        {
            [Value(0, Required = true, HelpText = "Input files with words.")]
            public string InputFiles { get; set; }

            [Value(1, Required = true, HelpText = "Result file.")]
            public string savePath { get; set; }

            [Option('w', "width", Default=1920, HelpText = "Width result image.")]
            public int width { get; set; }

            [Option('h', "height", Default = 1080, HelpText = "Height result image.")]
            public int height { get; set; }

            [Option('b', "background", Default ="White", HelpText = "Background color.")]
            public string backgroundColor { get; set; }

            [Option('f', "font", Default = "Comic Sans MS", HelpText = "Font name.")]
            public string fontName { get; set; }

            [Option('s', "splitter", Default = "WhiteSpace", HelpText = "Split by line or white space. (Line || WhiteSpace)")]
            public string splitType { get; set; }

            [Option('b', "boringWords", Default = "DefaultBoringWords", HelpText = "Split by line or white space. (Line || WhiteSpace)")]
            public string boringWordsPath { get; set; }

            // Omitting long name, defaults to name of property, ie "--verbose"
        }

        static void Main(string[] args)
        {
            string splitType = "";
            var container = new ContainerBuilder();
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts =>
              {
                  splitType = opts.splitType;
                  container.RegisterInstance(new TagCloudSettings(opts.InputFiles,
                      opts.savePath,
                      opts.boringWordsPath == "DefaultBoringWords" ? @"D:\проекты\SHPORA2019\di\TagsCloud\resources\NewBoringWords.txt" : opts.boringWordsPath,
                      opts.width,
                      opts.height,
                      Color.FromName(opts.backgroundColor),
                      opts.fontName)).AsSelf().SingleInstance() ;
              });
            var dataAccess = Assembly.GetExecutingAssembly();
            container.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();

            container.RegisterType<PathValidator>().AsSelf();
            container.RegisterType<SpliterByLine>().AsSelf();
            //container.RegisterType<AnglesCloudLayouter>().As<ITagCloudLayouter>();

            if (splitType == "Line")
                container.RegisterType<SpliterByLine>().As<ITextSpliter>();
            else if (splitType == "WhiteSpace")
                container.RegisterType<SpliterByWhiteSpace>().As<ITextSpliter>();
            else
            {
                throw new ArgumentException($"Unsupported split format {splitType}.");
            }

            container.RegisterType<TagCloudVisualizer>().AsSelf();
            container.RegisterType<WordValidatorSettings>().AsSelf();

            Container = container.Build();

            Container.Resolve<TagCloudVisualizer>().GenerateTagCloud();
        }
    }
}
