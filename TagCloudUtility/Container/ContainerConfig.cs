using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using TagCloud.Layouter;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.Tag.Container;
using TagCloud.Utility.Models.TextReader;
using TagCloud.Utility.Models.WordFilter;
using TagCloud.Utility.Runner;
using TagCloud.Visualizer;
using TagCloud.Visualizer.Drawer;
using TagCloud.Visualizer.Settings;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Utility.Container
{
    public static class ContainerConfig
    {
        public static IContainer StandartContainer => Configure(Options.Standart);
        public static IContainer Configure(Options options)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterInstance(new SolidColorizer(ColorTranslator.FromHtml(options.Brush)))
                .As<IColorizer>()
                .AsSelf();

            builder
                .RegisterType<DrawSettings>()
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(DrawFormat),
                    (pi, c) => options.DrawFormat)
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(Font),
                    (pi, c) => new FontConverter().ConvertFrom(options.Font))
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(Color),
                    (pi, c) => ColorTranslator.FromHtml(options.Color))
                .As<IDrawSettings>()
                .AsSelf();

            builder
                .RegisterType<Spiral>()
                .As<IPointsSequence>()
                .AsSelf();

            builder
                .RegisterType<CenterRectanglePlacer>()
                .As<IRectanglePlacer>()
                .AsSelf();

            var sizeArr = options.Size
                .Split('x')
                .Select(int.Parse)
                .ToArray();

            builder
                .Register(_ => new Size(sizeArr[0], sizeArr[1]))
                .As<Size>();

            builder
                .RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .AsSelf();

            builder
                .RegisterType<CloudDrawer>()
                .As<ICloudDrawer>()
                .AsSelf();

            builder
                .RegisterType<CloudVisualizer>()
                .As<ICloudVisualizer>()
                .AsSelf();

            builder
                .RegisterType<TagContainerReader>()
                .As<ITagContainerReader>()
                .Named<ITagContainerReader>(".txt")
                .Named<ITagContainerReader>(".ini")
                .AsSelf();

            if (options.PathToTags != null)
            {
                builder.Register(
                        c => c
                            .ResolveNamed<ITagContainerReader>(Helper.GetExtension(options.PathToTags))
                            .ReadTagsContainer(Helper.GetPath(options.PathToTags)))
                    .As<ITagContainer>()
                    .AsSelf();
            }
            else
            {
                builder
                    .RegisterInstance(
                        new TagContainer
                        {
                            {"Big", new FrequencyGroup(0.9, 1), 35},
                            {"Average", new FrequencyGroup(0.6, 0.9), 25},
                            {"Small", new FrequencyGroup(0, 0.6), 15}
                        })
                    .As<ITagContainer>()
                    .AsSelf();
            }

            builder
                .RegisterType<TagReader>()
                .As<ITagReader>()
                .AsSelf();

            builder
                .RegisterType<TxtReader>()
                .WithParameter((pi, c) => pi.ParameterType == typeof(string),
                    (pi, c) => Helper.GetExtension(options.PathToWords))
                .As<ITextReader>()
                .Named<ITextReader>(".txt")
                .Named<ITextReader>(".ini")
                .AsSelf();

            if (options.PathToStopWords != null)
            {
                builder
                    .RegisterType<WordFilter>()
                    .WithParameter(
                        (pi, c) => pi.ParameterType == typeof(IEnumerable<string>),
                        (pi, c) => c
                            .ResolveNamed<ITextReader>(Helper.GetExtension(options.PathToStopWords))
                            .ReadToEnd(Helper.GetPath(options.PathToStopWords)))
                    .As<IWordFilter>()
                    .AsSelf();
            }
            else
            {
                builder
                    .RegisterType<WordFilter>()
                    .As<IWordFilter>()
                    .AsSelf();
            }

            builder
                .RegisterType<TagCloudRunner>()
                .WithParameter(
                    (pi, c) => pi.Name == "pathToWords",
                    (pi, c) => options.PathToWords)
                .WithParameter(
                    (pi, c) => pi.Name == "pathToPicture",
                    (pi, c) => options.PathToPicture)
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(ImageFormat),
                    (pi, c) => Helper.GetImageFormat(options.PathToPicture))
                .As<ITagCloudRunner>()
                .AsSelf();

            return builder.Build();
        }
    }
}