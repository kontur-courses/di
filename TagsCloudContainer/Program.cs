using System;
using System.Drawing;
using System.Reflection;
using Autofac;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            containerBuilder.RegisterType<TagCloudCreater>().AsSelf();
            var container = containerBuilder.Build();
            var scope = container.BeginLifetimeScope();
            var creator = scope.Resolve<TagCloudCreater>();
            creator.SetFontRandomColor();
            creator.TrySetImageFormat("png");
            creator.SetFontFamily("Comic Sans MS");
            creator.SetImageSize(500);
            creator.Create("C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer\\input.txt",
                "C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer");
        }
    }
}
