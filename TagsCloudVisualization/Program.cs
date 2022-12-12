using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.FontSettings;
using TagsCloudVisualization.Frequency;
using TagsCloudVisualization.ImageRendering;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.Spirals;
using TagsCloudVisualization.Storages;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualizer;
using TagsCloudVisualization.WordProcessors;
using TagsCloudVisualization.WordProcessors.WordProcessingSettings;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args).Value;
            var builder = new ContainerBuilder();

            builder.RegisterType<FontSettings.FontSettings>().As<IFontSettings>().WithParameters(new[]
                {
                    new NamedParameter("fontColor", options.FontColor),
                    new NamedParameter("fontFamily", options.FontFamily)
                });

            builder.RegisterType<ImageRenderingSettings>().As<IImageRenderingSettings>()
                .WithParameters(new[]
                {
                    new NamedParameter("width", options.Width),
                    new NamedParameter("height", options.Height),
                });

            builder.RegisterType<Spiral>().As<ISpiral>().WithParameters(new[]
            {
                new NamedParameter("center", new Point(options.Width / 2, options.Height / 2)),
                new NamedParameter("angleStep", options.AngleStep)
            });
            
            builder.RegisterType<ImageSaver>().As<IImageSaver>().WithParameters(new[]
            {
                new NamedParameter("path", options.PathToOutputFile),
                new NamedParameter("fileName", options.OutputFileName),
                new NamedParameter("fileExtension", options.OutputFileExtension)
            });

            builder.RegisterType<TextReader>().As<ITextReader>().WithParameter("path", options.PathToInputFile);
            builder.RegisterType<ProcessingSettings>().As<IProcessingSettings>().WithParameter("excludedWords", options.ExcludedWords);

            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<ImageSaver>().As<ImageSaver>();
            builder.RegisterType<FilteredWordStorage>().As<IWordStorage>();
            builder.RegisterType<FrequencyCounter>().As<IFrequencyCounter>();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>();
            builder.RegisterType<CloudVisualizer>().As<IImageVisualizer>();
            
            var container = builder.Build();
            var visualizer = container.Resolve<IImageVisualizer>();
            var saver = container.Resolve<IImageSaver>();

            saver.Save(visualizer.CreateImage());
        }
    }
}


