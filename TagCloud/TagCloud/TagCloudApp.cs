using System;
using System.Collections.Generic;
using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class TagCloudApp
    {
        public static void Main(string[] args)
        {
            var container = new ContainerBuilder();

            container.RegisterType<TagCloudCreator>()
                     .AsSelf();

            container.RegisterType<CircularCloudLayouter>()
                     .AsSelf();

            container.RegisterTypes(typeof(CsvTextReader), typeof(TxtTextReader))
                     .As<ITextReader>();

            container.RegisterType<RoundSpiralGenerator>()
                     .As<ISpiralGenerator>();

            container.RegisterType<SimpleWordsPreparer>()
                     .As<IWordsPreparer>();

            container.RegisterType<TagCloudStatsGenerator>()
                     .As<ITagCloudStatsGenerator>();

            container.RegisterType<FileImageCreator>()
                     .As<ITagCloudImageCreator>();

            container.RegisterType<ConsoleUserInterface>()
                     .As<UserInterface>()
                     .SingleInstance();

            using (var scope = container.Build()
                                        .BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Run(args);
            }
        }
    }

    internal class CsvTextReader : ITextReader
    {
        public bool TryReadWords(string path, out IEnumerable<string> words) => throw new NotImplementedException();
    }
}
