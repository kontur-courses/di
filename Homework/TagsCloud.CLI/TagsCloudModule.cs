using System;
using System.Collections.Generic;
using Autofac;
using TagsCloud.Visualization;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.Drawer;
using TagsCloud.Visualization.FontFactory;
using TagsCloud.Visualization.ImagesSaver;
using TagsCloud.Visualization.LayoutContainer.ContainerBuilder;
using TagsCloud.Visualization.PointGenerator;
using TagsCloud.Visualization.WordsFilter;
using TagsCloud.Visualization.WordsParser;
using TagsCloud.Visualization.WordsReaders;
using TagsCloud.Visualization.WordsReaders.FileReaders;
using TagsCloud.Visualization.WordsSizeService;

namespace TagsCloud.Words
{
    public class TagsCloudModule : Module
    {
        private readonly TagsCloudModuleSettings settings;

        public TagsCloudModule(TagsCloudModuleSettings settings)
            => this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.Register(ctx => new FileReadService(settings.InputWordsFile,
                    ctx.Resolve<IEnumerable<IFileReader>>()))
                .As<IWordsReadService>();

            builder.RegisterType<WordsService>().As<IWordsService>();

            builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();
            builder.RegisterType<WordsParser>().As<IWordsParser>();

            builder.Register(_ => new FontFactory(settings.FontSettings)).As<IFontFactory>();
            builder.RegisterType<WordsSizeService>().As<IWordsSizeService>();
            builder.RegisterType<WordsContainerBuilder>().As<AbstractWordsContainerBuilder>();

            builder.Register(_ => new ArchimedesSpiralPointGenerator(settings.Center)).As<IPointGenerator>();
            builder.RegisterType(settings.LayouterType).As<ICloudLayouter>();

            builder.Register(_ => settings.LayoutVisitor).As<IContainerVisitor>();
            builder.RegisterType<Drawer>().As<IDrawer>();
            builder.Register(_ => new ImageSaver(settings.SaveSettings)).As<IImageSaver>();
        }
    }
}