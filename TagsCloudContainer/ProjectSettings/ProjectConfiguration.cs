using System;
using System.Reflection;
using Autofac;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Readers;

namespace TagsCloudContainer.ProjectSettings
{
    public static class ProjectConfiguration
    {
        public static IContainer GetConfiguration()
        {
            var builder = new ContainerBuilder();
           
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DocumentsReader>().As<IReader>().SingleInstance();
            builder.RegisterType<RandomCircularCloudLayouter>().As<ICircularCloudLayouter>().SingleInstance();
            builder.RegisterType<Random>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}