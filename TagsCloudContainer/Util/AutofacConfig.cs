using System.Collections.Generic;
using Autofac;
using System.Drawing;
using TagsCloudContainer.Cloud;
using TagsCloudVisualization;
using TagsCloudContainer.Words;

namespace TagsCloudContainer.Util
{
    class AutofacConfig
    {
        public static IContainer ConfigureContainer(string[] args)
        {
            var container = new ContainerBuilder();

            container
                .RegisterType<ArgumentsParser>()
                .AsSelf()
                .WithParameter("args", args)
                .SingleInstance();

            container
                .Register(c => 
                    new TextFileReader(c.Resolve<ArgumentsParser>()
                            .InputPath)
                    .ReadWords())
                .Named<string[]>("words").SingleInstance();

            container.Register(c =>
            {
                var parser = c.Resolve<ArgumentsParser>();
                if (parser.WordsToExclude == null)
                    return new HashSet<string>();
                return new TextFileReader(parser.WordsToExclude).ReadWordsInHashSet();
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

            container.Register(c => new WordPreprocessing(c.ResolveNamed<string[]>("words")))
                .As<WordPreprocessing>()
                .SingleInstance();           

            container.RegisterType<WordAnalizer>()
                .AsSelf()
                .SingleInstance();

            return container.Build();
        }
    }
}
