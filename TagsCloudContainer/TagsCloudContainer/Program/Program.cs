using Autofac;
using TagsCloudContainer.Models;

namespace TagsCloudContainer
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            var userHandler = new ConsoleUserHandler(args);
            var inputInfo = userHandler.GetInputInfo();
            var container = GetInjectionContainer(inputInfo);
            ExecuteProgram(container, inputInfo);
        }

        private static IContainer GetInjectionContainer(InputInfo inputInfo)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterInstance(new OnlyNounDullWordsEliminator())
                .As<IDullWordsEliminator>();
            builder.RegisterInstance(new TextFileReader(inputInfo.FileName)).As<ITextReader>();
            builder.RegisterType<DefaultTextHandler>().As<TextHandler>();
            builder.RegisterType<DefaultAlgorithm>().As<ITagCloudBuildingAlgorithm>();
            builder.RegisterType<TagCloudBuilder>().As<ITagCloudBuilder>();
            builder.RegisterInstance(new PictureInfo("tagCloud", inputInfo.ImageFormat)).AsSelf();
            builder.RegisterType<DefaultTagsPaintingAlgorithm>().As<ITagsPaintingAlgorithm>();
            builder.RegisterInstance(new CircularTagsCloudLayouter()).As<ITagsLayouter>();
            builder.RegisterType<TagCloudDrawer>().AsSelf();
            return builder.Build();
        }

        private static void ExecuteProgram(IContainer container, InputInfo inputInfo)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var drawer = scope.Resolve<TagCloudDrawer>();
                drawer.DrawTagCloud(inputInfo.MaxWordsCnt);
            }
        }
    }
}