using System.Reflection;
using Autofac;
using Microsoft.Office.Interop.Word;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Clients;
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
            return builder.Build();
        }
    }
}