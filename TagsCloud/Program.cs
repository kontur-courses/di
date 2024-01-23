using McMaster.Extensions.CommandLineUtils;

namespace TagsCloud;

public class Program
{
    [Option]
    public string Subject { get; set; }
    
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    // Application entry point
    private void OnExecute()
    {
        Console.WriteLine("Hello from command line application!");
        Console.WriteLine(Subject);
    }
}