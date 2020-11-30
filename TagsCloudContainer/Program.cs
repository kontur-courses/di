using Autofac;
using CommandLine;
using TagsCloudContainer.Drawer;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var builder = new ContainerBuilder();
            builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();
            builder.RegisterType<FileReader>().As<IWordReader>().WithParameter("filePath", options.FilePath);
            builder.RegisterType<Settings>().As<ISettings>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<LayoutDrawer>().As<ILayoutDrawer>();
            builder.RegisterType<RectangleLayout>().As<IRectangleLayout>();
            builder.RegisterType<TagCloudContainer>().As<ITagCloudContainer>();
            builder.RegisterInstance(options).As<IOptions>();
            var container = builder.Build();

            var tagCloudContainer = container.Resolve<ITagCloudContainer>();
            tagCloudContainer.MakeTagCloud();
            tagCloudContainer.SaveTagCloud();
        }
    }
}