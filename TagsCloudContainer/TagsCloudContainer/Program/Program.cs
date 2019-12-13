using System;
using System.Drawing;
using Autofac;
using com.sun.codemodel.@internal.fmt;
using edu.stanford.nlp.tagger.maxent;

namespace TagsCloudContainer
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var userHandler = new ConsoleUserHandler(args);
            var inputInfo = userHandler.GetInputInfo();
            builder.RegisterInstance(new OnlyNounDullWordsEliminator())
                .As<IDullWordsEliminator>();
            builder.RegisterInstance(new TextFileReader(inputInfo.FileName)).As<ITextReader>();
            builder.RegisterType<DefaultTextHandler>().As<TextHandler>();
            builder.RegisterType<DefaultAlgorithm>().As<ITagCloudBuildingAlgorithm>();
            builder.RegisterType<TagCloudBuilder>().As<ITagCloudBuilder>();
            builder.RegisterInstance(new PictureInfo("tagCloud", inputInfo.ImageFormat)).AsSelf();
            builder.RegisterType<DefaultTagsPaintingAlgorithm>().As<ITagsPaintingAlgorithm>();
            builder.RegisterInstance(new CircularTagsLayouter()).As<ITagsLayouter>();
            builder.RegisterType<TagCloudDrawer>().AsSelf();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var drawer = scope.Resolve<TagCloudDrawer>();
                drawer.DrawTagCloud(inputInfo.FileName, inputInfo.MaxWordsCnt);
                userHandler.WriteToUser(OutputLogger.GetAllLogs());
            }
        }
    }
}