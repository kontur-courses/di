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

            var container = builder.Build();

            if (args.Length == 0)
                args = new[]
                {
                    @"-p", @"C:\Users\Михаил\Desktop\di\TagCloud.Tests\Resources\words_with_different_delimiters.txt",
                    @"-i", @"C:\Users\Михаил\Desktop\heh.png",
                    @"--spiralstep", @"1"
                };

            Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                    {
                        //TODO: work with it more attractive
                        var textWorkingSettings = container.Resolve<TextWorkingSettings>();
                        options.UpdateTextWorkingSettings(textWorkingSettings);

                        var paintingSettings = container.Resolve<PaintingSettings>();
                        options.UpdatePaintingSettings(paintingSettings);

                        var visualizingSettings = container.Resolve<VisualizingSettings>();
                        options.UpdateVisualizingSettings(visualizingSettings);

                        var layoutingSettings = container.Resolve<LayoutingSettings>();
                        options.UpdateLayoutingSettings(layoutingSettings);

                        var tagCloudSettings = container.Resolve<TagCloudSettings>();
                        options.UpdateTagCloudSettings(tagCloudSettings);

                        var tagCloud = container.Resolve<Core.TagCloud>();
                        tagCloud.MakeTagCloudAndSave();
                    }
                );
        }
    }
}
