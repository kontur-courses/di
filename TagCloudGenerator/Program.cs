using CommandLine;
using TagCloudGenerator;

public class Program
{
    static TagCloudDrawer tagCloudDrawer = new TagCloudDrawer();
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "The path for the text file.")]
        public string Path { get; set; }
    }
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => tagCloudDrawer.DrawWordsCloud(o.Path));
    }
}