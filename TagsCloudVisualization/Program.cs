using Autofac;
using CommandLine;
using TagsCloudVisualization.CommandLine;

namespace TagsCloudVisualization;

public class Program
{
    public static void Main(string[] args)
    {
        var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;

        try 
        {
            var container = DiContainerBuilder.BuildContainer(options);
            var creator = container.Resolve<TagCloudCreator>();
            creator.CreateAndSaveImage();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}