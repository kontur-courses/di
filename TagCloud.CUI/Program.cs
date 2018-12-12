using System;
using System.Drawing;
using Autofac;
using CommandLine;
using TagCloud.Core.Layouters;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking;
using TagCloud.Core.TextWorking.WordsProcessing;
using TagCloud.Core.TextWorking.WordsProcessing.ProcessingUtilities;
using TagCloud.Core.TextWorking.WordsReading;
using TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles;
using TagCloud.Core.Util;
using TagCloud.Core.Visualizers;

namespace TagCloud.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            InjectDependencies(builder);
            var container = builder.Build();

            if (args.Length == 0)
                args = new[]
                {
                    @"-p", @"test_words.txt",
                    @"-i", @"result.bmp",
                    //@"--spiralstep", @"1"
                };

            Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                    {
                        var textWorkingSettings = container.Resolve<TextWorkingSettings>();
                        var paintingSettings = container.Resolve<PaintingSettings>();
                        var visualizingSettings = container.Resolve<VisualizingSettings>();
                        var layoutingSettings = container.Resolve<LayoutingSettings>();
                        var tagCloudSettings = container.Resolve<TagCloudSettings>();
                        options.UpdateSettings(textWorkingSettings, paintingSettings, visualizingSettings,
                            layoutingSettings, tagCloudSettings);

                        var tagCloud = container.Resolve<Core.TagCloud>();
                        tagCloud.MakeTagCloudAndSave();
                    }
                );
        }

        static void InjectDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<Core.TagCloud>().AsSelf();
            builder.RegisterType<TagCloudSettings>().AsSelf().SingleInstance();

            builder.RegisterType<TextWorker>().AsSelf();
            builder.RegisterType<TxtWordsReader>().As<IWordsReaderForFile>();
            builder.RegisterType<GeneralWordsReader>().As<IWordsReader>();
            builder.RegisterType<LowerCaseUtility>().As<IProcessingUtility>();
            builder.RegisterType<SimpleWordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<TextWorkingSettings>().AsSelf().SingleInstance();

            builder.RegisterType<SimpleTagCloudVisualizer>().As<ITagCloudVisualizer>();
            builder.RegisterType<VisualizingSettings>().AsSelf().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<LayoutingSettings>().AsSelf().SingleInstance();
            builder.RegisterType<OneColorPainter>().As<IPainter>();
            builder.RegisterType<PaintingSettings>().AsSelf().SingleInstance();
        }
    }
}
