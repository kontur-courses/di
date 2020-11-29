using Autofac;
using CommandLine;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var parsedArguments = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var builder = new ContainerBuilder();
            builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();
            builder.RegisterType<FileReader>().As<IWordReader>().WithParameter("filePath", parsedArguments.FilePath);
            builder.RegisterType<Settings>().As<ISettings>();
            builder.RegisterType<TagCloudContainer>().As<ITagCloudContainer>();
            var container = builder.Build();
            
            var tagCloudContainer = container.Resolve<ITagCloudContainer>();
            tagCloudContainer.MakeTagCloud();
        }
    }
}