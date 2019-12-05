using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudContainer.Core;
using TagsCloudContainer.ResultProcessing;
using TagsCloudContainer.UserInterface;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = GetDependencyInjectionContainerBuilder();
            var parametersProvider = containerBuilder.Build().Resolve<IParametersProvider>();
            if (parametersProvider.TryGetParameters(args, out var parameters))
            {
                var newContainerBuilder = GetDependencyInjectionContainerBuilder();
                var container = UpdateContainerBuilderWithParameters(newContainerBuilder, parameters);
                var tagCloudVisualizer = container.Resolve<ITagCloudVisualizer>();
                var bitmap = tagCloudVisualizer.GetTagCloudBitmap(parameters);
                var resultProcessor = container.Resolve<IResultProcessor>();
                resultProcessor.ProcessResult(bitmap, parameters.OutputFilePath);
            }
        }

        private static ContainerBuilder GetDependencyInjectionContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterInstance(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName,
                "WordProcessing", "Filtering", "PartsOfSpeechQualifying", "MyStem")).As<string>();

            return builder;
        }

        private static IContainer UpdateContainerBuilderWithParameters(ContainerBuilder builder, Parameters parameters)
        {
            builder.RegisterInstance(parameters)
                .As<Parameters>();
            builder.Register(c => c.Resolve<Parameters>().Colors)
                .As<List<Color>>();

            return builder.Build();
        }
    }
}
