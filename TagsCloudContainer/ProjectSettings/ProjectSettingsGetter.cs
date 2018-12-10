using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudContainer.BoringWordsGetters;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Clients;
using TagsCloudContainer.FontSizesChoosers;
using TagsCloudContainer.ImageCreators;
using TagsCloudContainer.ImageSavers;
using TagsCloudContainer.Readers;
using TagsCloudContainer.RectanglesFilters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsFilters;
using TagsCloudContainer.WordsHandlers;
using TagsCloudContainer.WordsTransformers;

namespace TagsCloudContainer.ProjectSettings
{
    public static class ProjectSettingsGetter
    {
        public static IContainer GetSettings()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new Size(600, 800));
            builder.Register(c => new FontFamily("Arial"));
            builder.Register(c => new Palette(Color.Aqua, Color.Green, Color.White)).SingleInstance();
            builder.RegisterType<PrepositionAndPronounsGetter>().As<IBoringWordsGetter>();
            builder.RegisterType<FilterRectanglesWithNegativeCoordinates>().As<IFilter<Rectangle>>();
            builder.RegisterType<BoringWordsExcluder>().As<IFilter<string>>();
            builder.RegisterType<WordsTransformerToLowerCase>().As<IWordsTransformer>();
            builder.RegisterType<CircularPointsChooser>().As<IEnumerator<Point>>();
            builder.RegisterType<WordsHandler>().AsSelf();
            builder.Register(c => new FontSizeChooser(30)).As<IFontSizeChooser>();
            builder.RegisterType<SingleColorChooser>().As<IColorChooser>();
            builder.RegisterType<TextSettings>().AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var settings = c.Resolve<ImageSettings>();
                var size = settings.ImageSize;
                return new Point(size.Width / 2, size.Height / 2);
            });
            builder.RegisterType<CircularCloudLayouter>().AsSelf();
            builder.Register<ICircularCloudLayouter>(c => c.Resolve<CircularCloudLayouter>())
                .As<ICircularCloudLayouter>();
            builder.RegisterType<ImageSettings>()
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<SettingsManager>().AsSelf();
            builder.RegisterType<ImageCreator>().AsSelf();
            builder.RegisterType<ImageSaver>().As<IImageSaver>().AsSelf();
            builder.Register<IEnumerable<string>>((c, p) =>
            {
                var fileName = p.TypedAs<string>();
                return new TextFileReader(fileName);
            });
            builder.RegisterType<ConsoleClient>().As<IClient>();
            return builder.Build();
        }
    }
}