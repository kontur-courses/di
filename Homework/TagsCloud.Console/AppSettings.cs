using CommandLine;

namespace TagsCloud.Console
{
    public class AppSettings : IAppSettings
    {
        [Option('i', "inputPath", Required = true, HelpText = "File path which provides words for tags")]
        public string InputPath { get; set; } = null!;

        [Option('o', "outputPath", Default = "examples/output.png", HelpText = "Directory path for tag cloud")]
        public string OutputPath { get; set; } = null!;


        public static IAppSettings Parse(string[] args)
        {
            return ArgumentsParser.Parse<AppSettings>(args);
        }
    }
}