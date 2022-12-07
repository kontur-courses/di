using CommandLine;

namespace ConsoleApp;

public class ArgsParser
{
    public class Options
    {
        [Option('g', "gui", Required = false, HelpText = "Run in GUI mode")]
        public bool IsInGuiMode { get; set; }

        [Option('s', "size", Required = false, HelpText = "Image size. Example: 1024, 1024")]
        public string Size { get; set; }

        [Option('h', "help", Required = false, HelpText = "Help")]
        public bool IsHelpRequired { get; set; }
    }

    private static void ParseArgs(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                if (o.IsHelpRequired)
                {
                    Console.WriteLine("Commands:");
                    foreach (var propertyInfo in typeof(Options).GetProperties())
                    {
                        var attribute =
                            (OptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(OptionAttribute));
                        if (attribute is null)
                            continue;
                        Console.WriteLine($"-{attribute?.ShortName}\t--{attribute?.LongName}\t{attribute?.HelpText}");
                    }
                }

                if (o.IsInGuiMode)
                {
                    Console.WriteLine("Loading GUI...");
                }
            });
    }
}