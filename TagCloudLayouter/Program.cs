using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagCloudLayouter
{
    class Program
    {
        private static IContainer container;

        private static void InitContainer(Point center)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleClient>().As<IUserInterface>();
            //builder.RegisterType<PreprocessorLinear>().As<IPreprocessor>();
            builder.Register(ctx => new SimplePreprocessor(new XmlWordExcluder())).As<IPreprocessor>();
            builder.RegisterType<TxtReader>().As<IReader>();
            //builder.RegisterType<LinesWithWordsParser>().As<ITextParser>();
            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.Register(ctx => new ArchimedesSpiral(center)).As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            container = builder.Build();
        }

        static void Main(string[] args)
        {
            var client = new ConsoleClient();
            var config = client.GetConfig(args);
            InitContainer(config.Center);

            var layouter = container.Resolve<ICloudLayouter>();
            var proc = container.Resolve<IPreprocessor>();
            var text = container.Resolve<IReader>().ReadFromFile(config.InputFile);
            var allWords = container.Resolve<ITextParser>().GetWords(text);
            var validWords = proc
                .GetValidWords(allWords)
                .Take(config.Count)
                .ToList();

            var vis = new TagCloudVisualization(layouter);
            vis.SaveTagCloud(config.FileName, config.OutPath, config.Font, validWords);
        }
    }
}
