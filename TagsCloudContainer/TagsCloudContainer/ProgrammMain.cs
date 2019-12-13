using System;
using System.Drawing;
using Autofac;
using com.sun.codemodel.@internal.fmt;
using edu.stanford.nlp.tagger.maxent;

namespace TagsCloudContainer
{
    public class ProgrammMain
    { 
        public static void Execute(ConsoleParser.StandartOptions parsedArgs)
        {
            Execute(parsedArgs.File, parsedArgs.MaxCnt, parsedArgs.Format);
        }

        public static void Execute(string textFile, int maxWordsCount = int.MaxValue,
            string imageFormat = "png")
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new OnlyNounDullWordsEliminator())
                .As<IDullWordsEliminator>();
            builder.RegisterType<FileHandler>().AsSelf();
            builder.RegisterType<DefaultAlgorithm>().As<IBuildingAlgorithm>();
            builder.RegisterType<TagCloudBuilder>().AsSelf();
            builder.RegisterInstance(new PictureInfo("tagCloud", new Point(0, 0), imageFormat)).AsSelf();
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