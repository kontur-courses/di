using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagCloudCreation;
using TagCloudVisualization;

namespace TagCloudApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new ContainerBuilder();

            container.RegisterType<TagCloudCreator>()
                     .AsSelf();

            container.RegisterType<CircularCloudLayouter>()
                     .AsSelf();

            container.RegisterTypes(typeof(CsvTextReader), typeof(TxtTextReader))
                     .As<ITextReader>();

            container.RegisterType<RoundSpiralGenerator>()
                     .As<AbstractSpiralGenerator>();

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

    internal class TxtTextReader : ITextReader
    {
        public bool TryReadWords(string path, out IEnumerable<string> words)
        {
            words = null;
            try
            {
                var text = File.ReadAllText(path);
                words = text.Split(null);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public string Extension => ".txt";
    }

    internal class CsvTextReader : ITextReader
    {
        public bool TryReadWords(string path, out IEnumerable<string> words) => throw new NotImplementedException();

        public string Extension => ".csv";
    }

    internal class ConsoleUserInterface : UserInterface
    {
        public ConsoleUserInterface(TagCloudCreator creator, IEnumerable<ITextReader> readers) : base(creator, readers)
        {
        }

        public override void Run(string[] startupArgs)
        {
            throw new NotImplementedException();
        }
    }

    internal interface ITextReader
    {
        bool TryReadWords(string path, out IEnumerable<string> words);
        string Extension { get; }

    }
}

