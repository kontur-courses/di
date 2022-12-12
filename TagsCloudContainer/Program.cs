using Autofac;
using CommandLine;
using TagsCloudContainer.Application;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args).Value;
            var container = Container.Container.SetDiBuilder(options);
            var app = container.Resolve<IApp>();
            app.Run();
        }
    }
}