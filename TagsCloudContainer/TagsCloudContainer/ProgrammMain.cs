using System.Drawing;
using Autofac;
using edu.stanford.nlp.tagger.maxent;

namespace TagsCloudContainer
{
    public class ProgrammMain
    {
        private static string textFile;
        private static int maxWordsCount = int.MaxValue;

        public static void Execute(ConsoleParser.StandartOptions parsedArgs)
        {
            textFile = parsedArgs.File;
            maxWordsCount = parsedArgs.MaxCnt;
            Execute();
        }

        public static void Execute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new OnlyNounDullWordsEliminator())
                .As<IDullWordsEliminator>();
            builder.RegisterType<FileHandler>().AsSelf();
            builder.RegisterType<DefaultAlgorithm>().As<IBuildingAlgorithm>();
            builder.RegisterType<TagCloudBuilder>().AsSelf();
            builder.RegisterInstance(new PictureInfo("tagCloud", new Point(0, 0))).AsSelf();
            builder.RegisterType<DefaultPaintingAlgorithm>().As<IPaintingAlgorithm>();
            builder.RegisterType<TagCloudDrawer>().AsSelf();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var drawer = scope.Resolve<TagCloudDrawer>();
                drawer.DrawTagCloud(textFile, maxWordsCount);
            }
        }
    }
}