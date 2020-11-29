using CommandLine;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var parsedArguments = Parser.Default.ParseArguments<CommandLineOptions>(args);
        }
    }
}