using Autofac;
using CommandLine;
using TagsCloudBuilder.Drawer;

namespace TagsCloudBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(BuildAndRun);
        }

        private static void BuildAndRun(Options options)
        {
            using (var container = TagCloudConsole.BuildContainer(options))
            {
                var drawer = container.Resolve<IDrawer>();
                drawer.DrawAndSaveWords();
            }
        }
    }
}
