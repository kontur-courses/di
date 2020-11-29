using Autofac;
using CommandLine;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();
            builder.RegisterType<FileReader>().As<IWordReader>().WithParameter("filePath", "file.txt");
            builder.RegisterType<Settings>().As<ISettings>();

            var parsedArguments = Parser.Default.ParseArguments<CommandLineOptions>(args);
        }
    }
}