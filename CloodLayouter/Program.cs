using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
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
            logicBuilder.RegisterType<Converter>();
            
            logicBuilder.RegisterType<FileStreamReader>().Named<IWordProvider>("FileStreamReader").SingleInstance();
            logicBuilder.RegisterType<WordSelector>()
                .WithParameter(ResolvedParameter.ForNamed<IWordProvider>("FileStreamReader")).SingleInstance();

            logicBuilder.RegisterType<WordSelector>().Named<IWordProvider>("WordSelector").SingleInstance();
            logicBuilder.RegisterType<Converter>().As<ITagProvider>()
                .WithParameter(ResolvedParameter.ForNamed<IWordProvider>("WordSelector")).SingleInstance();

            logicBuilder.RegisterType<Converter>().Named<ITagProvider>("Converter");
            logicBuilder.RegisterType<TagCloudDrawer>()
                .WithParameter(ResolvedParameter.ForNamed<ITagProvider>("Converter"));
            logicBuilder.RegisterType<ImagePerfomer>();
            
            var logicContainer = logicBuilder.Build();

            
            logicContainer.Resolve<ImagePerfomer>().DrawAndSave();
        }
    }
}