using System.Collections.Generic;
using Autofac;
using System.Drawing;
using TagsCloudContainer.Cloud;
using TagsCloudVisualization;
using TagsCloudContainer.Words;
using TagsCloudContainer.Arguments;

namespace TagsCloudContainer.Util
{
    class AutofacContainer
    {
        private IContainer container;
        public TagCloud TagCloud => container.Resolve<TagCloud>();
        public Brush Brush => container.Resolve<Brush>();
        public string FontName => container.ResolveNamed<string>("FontName");
        public string OutputPath => container.Resolve<ArgumentsParser>().OutputPath;

        public AutofacContainer(string[] args)
        {
            var container = new ContainerBuilder();

            container
                .RegisterType<ArgumentsParser>()
                .AsSelf()
                .WithParameter("args", args)
                .SingleInstance();

            container
                .Register(c =>
                    new TextFileWordsReader(c.Resolve<ArgumentsParser>()
                            .InputPath)
                    .ReadWords())
                .Named<string[]>("words").SingleInstance();

            container.Register(c =>
            {
                var parser = c.Resolve<ArgumentsParser>();
                if (parser.WordsToExcludePath == null)
                    return new HashSet<string>();
                return new TextFileWordsReader(parser.WordsToExcludePath).ReadWordsInHashSet();
            })
                .Named<HashSet<string>>("WordsToExclude")
                .SingleInstance();

            container
                .Register(c => c.Resolve<ArgumentsParser>().FontName)
                .Named<string>("FontName")
                .SingleInstance();

            container.Register(c => c.Resolve<ArgumentsParser>().Brush)
                .As<Brush>()
                .SingleInstance();

            container
                .RegisterType<TagCloud>()
                .As<TagCloud>()
                .SingleInstance();

            container
                .RegisterType<SuperTightCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter("center", new Point(1000, 1000))
                .SingleInstance();

            container.Register(c => 
                    new WordPreprocessing(c.ResolveNamed<string[]>("words"))
                        .ToLower()
                        .Exclude(c.ResolveNamed<HashSet<string>>("WordsToExclude"))
                        .IgnoreInvalidWords())
                .As<WordPreprocessing>()
                .SingleInstance();

            container.RegisterType<WordAnalizer>()
                .AsSelf()
                .SingleInstance();

            this.container = container.Build();
        }
    }
}
