using System.Diagnostics;
using Fclp;
using TagsCloudContainer.utility;

namespace TagsCloudContainer.UI;

public class CLI : IUI
{
    public ApplicationArguments Setup(string[] args)
    {
        var p = new FluentCommandLineParser<ApplicationArguments>();

        p.Setup(arg => arg.Input)
            .As('i', "input")
            .Required()
            .WithDescription("Input file path");

        p.Setup(arg => arg.Output)
            .As('o', "output")
            .Required()
            .WithDescription("Output file path");

        p.Setup(arg => arg.FontPath)
            .As("fontpath")
            .Required()
            .WithDescription("Font path");

        p.Setup(arg => arg.FontSize)
            .As("fontsize")
            .SetDefault(30)
            .WithDescription("Font path");

        p.Setup(arg => arg.Format)
            .As("format")
            .SetDefault("jpg")
            .WithDescription("Output file format");

        p.Setup(arg => arg.Resolution)
            .As('r', "resolution")
            .SetDefault([1920, 1080])
            .WithDescription("Output file resolution");

        p.Setup(arg => arg.Center)
            .As('c', "center")
            .SetDefault([960, 540])
            .WithDescription("Cloud center");

        p.Setup(arg => arg.Background)
            .As('b', "background")
            .SetDefault([255, 255, 255])
            .WithDescription("Background color");

        p.Setup(arg => arg.Scheme)
            .As('s', "scheme")
            .SetDefault([0, 0, 0, 255])
            .WithDescription("Scheme color");

        p.Setup(arg => arg.Exclude)
            .As('e', "exclude")
            .SetDefault(Utility.GetAbsoluteFilePath("src/boringWords.txt"))
            .WithDescription("Exclude words path");

        p.SetupHelp("?", "help")
            .Callback(text => Console.WriteLine(text));

        var result = p.Parse(args);

        if (result.HelpCalled) Environment.Exit(0);
        if (result.HasErrors) throw new ArgumentNullException();

        var obj = p.Object;

        return obj;
    }

    public void View(string output)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = output,
            UseShellExecute = true
        }); 
    }
}