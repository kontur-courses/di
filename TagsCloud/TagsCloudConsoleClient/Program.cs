using Autofac;
using TagsCloud.FileReader;
using TagsCloud.WordProcessing;
using CommandLine;
using TagsCloud.Interfaces;
using System.Drawing;
using TagsCloud.FinalProcessing;
using System;
using TagsCloud.CloudLayouter;

namespace TagsCloud.TagsCloudConsoleClient
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

            [Option('s', "spliter", Default = "WhiteSpace", HelpText = "Split by line or white space. (Line || WhiteSpace)")]
            public string splitType { get; set; }

            [Option('b', "boringWords", Default = "", HelpText = "Path to file with boring words. Words must be separated by line.")]
            public string boringWordsPath { get; set; }

            [Option('i', "ignoredPartsOfSpeech", Default = "ADVPRO,APRO,CONJ,INTJ,PR,PART,SPRO", HelpText = "Parts of speech to be excluded. " +
                "Possible parts: A, ADV, ADVPRO, ANUM, APRO, COM, CONJ, INTJ, NUM, PART, PR, S, SPRO, V.")]
            public string ignoredPartsOfSpeech { get; set; }

            [Option('g', "GenerationAlgorithm", Default = "CircularCloud", HelpText = "Which algorithm will be used to generate the cloud. " +
                "(CircularCloud || MiddleCloud)")]
            public string GenerationAlgoritm { get; set; }

            [Option('c', "ColorScheme", Default = "RandomColor", HelpText = "Color scheme of result cloud." +
                "(RandomColor || RedGreenBlueScheme)")]
            public string ColorScheme { get; set; }
        }

        static void Main(string[] args)
        {
            var container = new ContainerBuilder();
            Type tagLayouterType = null;
            Type textSpliterType = null;
            Type colorScheme = null;
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed(opts =>
              {
                  var imageFormatResult = TypesCollector.GetFormatFromPathSaveFile(opts.savePath);
                  if (imageFormatResult == null)
                      throw new ArgumentException("Unsupported image format.");
                  container.RegisterInstance(new TagCloudSettings(opts.InputFiles,
                      opts.savePath,
                      opts.boringWordsPath,
                      opts.width,
                      opts.height,
                      Color.FromName(opts.backgroundColor),
                      opts.fontName,
                      opts.ignoredPartsOfSpeech.Split(","),
                      opts.GenerationAlgoritm, 
                      imageFormatResult)).AsSelf().SingleInstance();
                  tagLayouterType = TypesCollector.GetTypeGeneationLayoutersByName(opts.GenerationAlgoritm);
                  if (tagLayouterType == null)
                      throw new ArgumentException($"Unknown generation algoritm {opts.GenerationAlgoritm}");
                  textSpliterType = TypesCollector.GetTypeSpliterByName(opts.splitType);
                  if (textSpliterType == null)
                      throw new ArgumentException($"Unsupported split format {opts.splitType}.");
                  colorScheme = TypesCollector.GetColorSchemeByName(opts.ColorScheme);
                  if (colorScheme == null)
                      throw new ArgumentException($"Unsupported color scheme {opts.ColorScheme}.");
              });
            RegistrAllTypes(container, tagLayouterType, textSpliterType, colorScheme);
            Container = container.Build();
            Container.Resolve<TagCloudVisualizer>().GenerateTagCloud();
        }

        private static void RegistrAllTypes(ContainerBuilder container, Type tagLayouterType, Type textSpliterType, Type colorScheme)
        {
            container.RegisterType<PathValidator>().AsSelf();
            container.RegisterType<SpliterByLine>().AsSelf();
            container.RegisterType<WordStream>().As<IWordStream>().SingleInstance();
            container.RegisterType<TagGenerator>().As<ITagGenerator>().SingleInstance();
            container.RegisterType<TagCloudGenerator>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<DefaultCloudDrawer>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<ImageSaver>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<DictionaryBasedCounter>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<TxtReader>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<ToLowerWordHandler>().AsImplementedInterfaces();
            container.RegisterType<TagCloudVisualizer>().AsSelf().SingleInstance();
            container.RegisterType<WordValidatorSettings>().AsSelf().SingleInstance();
            container.RegisterType(tagLayouterType).As<ITagCloudLayouter>().SingleInstance();
            container.RegisterType(textSpliterType).As<ITextSpliter>().SingleInstance();
            container.RegisterType<DefaultWordValidator>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType<DefaultFontGenerator>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType(colorScheme).AsImplementedInterfaces().SingleInstance();
        }
    }
}
