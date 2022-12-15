using CommandLine;
using TagCloud.Common;

namespace TagCloud.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(StartApp).WithNotParsed(HandleErrors);
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.Tag);
            }
        }

        private static void StartApp(Options cmdOptions)
        {
            var visualizationOptions = cmdOptions.MapToVisualizationOptions();
            try
            {
                CloudGeneratorApplication.Run(visualizationOptions);
                Console.WriteLine("Cloud created!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}