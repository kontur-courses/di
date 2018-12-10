using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using Autofac.Core;
using TagsCloud.CloudStructure;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.WordPrework;
using CommandLine;

namespace TagsCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }

        static IContainer BuildContainer(Options options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReader>().As<IWordsGetter>().WithParameter("fileName", options.File);
            builder.RegisterType<WordAnalyzer>().As<IWordAnalyzer>().WithParameter("useInfinitiveForm", options.Infinitive);

            builder.RegisterType<SpiralPointGenerator>().As<IPointGenerator>().WithParameter("dAngle", options.DAngle);
            builder.RegisterType<PointCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point()).SingleInstance(); ;

            builder.RegisterType<TagCloudLayouter>().As<ITagCloudLayouter>().WithParameters(new List<Parameter>
            {
                new NamedParameter("fontFamily", new FontFamily(options.Font)),
                new NamedParameter("minFontSize",options.MinFontSize),
                new NamedParameter("maxFontSize", options.MaxFontSize)
            });
            builder.RegisterType<TagsCloudVisualizer>().As<ITagsCloudVisualizer>().WithParameters(new List<Parameter>
            {
                new NamedParameter("pictureSize", new Size(options.Width, options.Height)),
                new NamedParameter("fontName", options.Font),
                new NamedParameter("backgroundColor", Color.FromName(options.BackgroundColor)),
                new NamedParameter("fontColor", Color.FromName(options.FontColor))
            });
            return builder.Build();
        }

        static void RunOptions(Options options)
        {
            var container = BuildContainer(options);
            var wordAnalyzer = container.Resolve<IWordAnalyzer>();

            var frequency = options.PartsToUse == null
                ? wordAnalyzer.GetSpecificWordFrequency(options.PartsToUse)
                : wordAnalyzer.GetWordFrequency(new HashSet<PartOfSpeech>(options.BoringParts));
            var visualizer = container.Resolve<ITagsCloudVisualizer>();
            var tags = container.Resolve<ITagCloudLayouter>().GetTags(frequency);
            var bitmap = visualizer.GetCloudVisualization(tags);
            var name = Path.GetFileName(options.File);
            var newName = Path.ChangeExtension(name, "jpg");
            bitmap.Save(newName, ImageFormat.Jpeg);
        }
    }
}
