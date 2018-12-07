using CommandLine;

namespace TagsCloudContainer.UI
{
    public class ConsoleUI : IUI
    {
        public (string, string) RetrievePaths(string[] args)
        {
            (string, string) result = (null, null);
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                result = (o.TextFile, o.ImageFile);
            });

            return result;
        }
    }

    public class Options
    {
        [Option("text", Default = "Resources\\sample.txt", Required = false, HelpText = "File to read text from")]
        public string TextFile { get; set; }

        [Option("image", Default = "result.png", Required = false, HelpText = "Output image file")]
        public string ImageFile { get; set; }
    }
}