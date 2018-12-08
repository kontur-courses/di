using System.Reflection;
using Autofac;
using CloodLayouter.App;
using CloodLayouter.Infrastructer;

namespace CloodLayouter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var logicBuilder = new ContainerBuilder();

            logicBuilder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces().SingleInstance();

            logicBuilder.RegisterType<FileWordProvider>().SingleInstance();
            logicBuilder.RegisterType<FileStreamReader>().SingleInstance();
            logicBuilder.RegisterType<Converter>().SingleInstance();
            logicBuilder.RegisterType<TagProvider>().SingleInstance();
            logicBuilder.RegisterType<TagCloudDrawer>().SingleInstance();

            var logicContainer = logicBuilder.Build();

            logicContainer.Resolve<IStreamReader>().Read();
            logicContainer.Resolve<IWordSlector>().Select();
            logicContainer.Resolve<IConverter>().Convert();
            logicContainer.Resolve<IDrawer>().Draw();
            logicContainer.Resolve<IImageSaver>().Save();
        }
    }
}