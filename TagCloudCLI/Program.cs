using Autofac;
using TagsCloud.FileReader;
using CommandLine;
using TagsCloud.Interfaces;
using System.Drawing;
using System;
using MyStemWrapper;
using System.IO;
using TagsCloud.PathValidators;
using TagsCloud.Spliters;
using TagsCloud.WordStreams;
using TagsCloud.TagGenerators;
using TagsCloud.WordCounters;
using TagsCloud.WordHandlers;
using TagsCloud.WordValidators;
using TagsCloud.FontGenerators;
using TagsCloud.CloudDrawers;
using TagsCloud.ImageSavers;
using TagsCloud.TagCloudGenerators;
using TagsCloud;

namespace TagCloudCLI
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            TagCloudSettings settings = null;
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed(opts =>
              {
                  var imageFormatResult = TypesCollector.GetFormatFromPathSaveFile(opts.savePath);
                  if (imageFormatResult == null)
                      throw new ArgumentException($"Unsupported image format.");
                  settings = new TagCloudSettings(opts.InputFiles,
                      opts.savePath,
                      opts.boringWordsPath,
                      opts.width,
                      opts.height,
                      Color.FromName(opts.backgroundColor),
                      opts.fontName,
                      opts.ignoredPartsOfSpeech.Split(",", StringSplitOptions.RemoveEmptyEntries),
                      opts.GenerationAlgoritm,
                      opts.splitType,
                      opts.ColorScheme,
                      imageFormatResult);
              });
            Container = RegisterAllTypes(settings);
            Container.Resolve<TagCloudVisualizer>().GenerateTagCloud();
        }

        private static IContainer RegisterAllTypes(TagCloudSettings settings)
        {
            var tagLayouterType = TypesCollector.GetTypeGeneationLayoutersByName(settings.generationAlgoritmName);
            var textSpliterType = TypesCollector.GetTypeSpliterByName(settings.spliterName);
            var colorScheme = TypesCollector.GetColorSchemeByName(settings.colorSchemeName);
            if (tagLayouterType == null)
                throw new ArgumentException($"Unknown generation algoritm {settings.generationAlgoritmName}.");
            if (textSpliterType == null)
                throw new ArgumentException($"Unsupported split format {settings.spliterName}.");
            if (colorScheme == null)
                throw new ArgumentException($"Unsupported color scheme {settings.colorSchemeName}.");
            var container = new ContainerBuilder();
            container.RegisterInstance(settings).AsSelf().SingleInstance();
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
            container.RegisterType<SimpleFontGenerator>().AsImplementedInterfaces().SingleInstance();
            container.RegisterType(colorScheme).AsImplementedInterfaces().SingleInstance();
            container.RegisterInstance(new MyStem {PathToMyStem = (Path.Combine(Path.GetFullPath(Path.Combine("Resources", "mystem.exe")))), Parameters = "-li" })
                .AsSelf().SingleInstance();
            return container.Build();
        }
    }
}
