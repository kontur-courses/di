using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;

namespace TagsCloudContainer;
#pragma warning disable CA1416

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var parsedArgument = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            AppDIInitializer.CreateCurveInstance(parsedArgument.Step, parsedArgument.Density, parsedArgument.Start);
            var checkedArguments = new ArgsChecker().Check(parsedArgument);
            
            var drawingModel = AppDIInitializer.Container
                .GetService<IDrawingModel>()
                .GetDrawingModel(checkedArguments);
            
            AppDIInitializer.Container
                .GetService<LayoutDrawer>()
                .Draw(drawingModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
#pragma warning restore CA1416