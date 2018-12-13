using System.Collections.Generic;
using System.Drawing;
using Autofac;
using Autofac.Core;
using TagsCloud.CloudStructure;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.TagsCloudVisualization.ColorSchemes;
using TagsCloud.WordPrework;


namespace TagsCloud
{
    public class Ioc
    {
        public static IContainer Configure(Options options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReader>().As<IWordsGetter>().WithParameter("fileName", options.File);
            builder.RegisterType<WordAnalyzer>().As<IWordAnalyzer>().WithParameter("useInfinitiveForm", options.Infinitive);

            builder.RegisterType<SpiralPointGenerator>().As<IPointGenerator>().WithParameter("dAngle", options.DAngle);
            builder.RegisterType<PointCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point()).SingleInstance();

            RegisterSizeDefiner(builder, options);
            builder.RegisterType<TagCloudLayouter>().As<ITagCloudLayouter>();
            RegisterColorScheme(builder, options);
            builder.RegisterType<TagsCloudVisualizer>().As<ITagsCloudVisualizer>().WithParameters(new List<Parameter>
            {
                new NamedParameter("pictureSize", new Size(options.Width, options.Height)),
                new NamedParameter("fontName", options.Font),
                new NamedParameter("backgroundColor", Color.FromName(options.BackgroundColor)),

            });
            builder.RegisterType<Application>().AsSelf();

            return builder.Build();
        }

        private static void RegisterColorScheme(ContainerBuilder builder, Options options)
        {
            switch (options.ColorScheme)
            {
                case ColorScheme.Random:
                    builder.RegisterType<RandomColorScheme>().As<IColorScheme>();
                    break;
                case ColorScheme.Red:
                    builder.RegisterType<RedColorScheme>().As<IColorScheme>();
                    break;
            }
        }

        private static void RegisterSizeDefiner(ContainerBuilder builder, Options options)
        {
            var sizeDefinerParameters = new List<Parameter>
            {
                new NamedParameter("fontName", options.Font),
                new NamedParameter("minFontSize", options.MinFontSize),
                new NamedParameter("maxFontSize", options.MaxFontSize)
            };

            switch (options.SizeDefinerType)
            {
                case SizeDefiner.Frequency:
                    builder.RegisterType<FrequencySizeDefiner>().As<ISizeDefiner>().WithParameters(sizeDefinerParameters);
                        break;
                case SizeDefiner.Random:
                    builder.RegisterType<RandomSizeDefiner>().As<ISizeDefiner>().WithParameters(sizeDefinerParameters);
                        break;
            }
        }
    }
}
