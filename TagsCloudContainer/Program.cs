using System;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudContainer.Core;
using TagsCloudContainer.ResultProcessing;
using TagsCloudContainer.UserInterface;
using TagsCloudContainer.WordProcessing.Converting;

namespace TagsCloudContainer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = GetDependencyInjectionContainer();
            var parametersProvider = container.Resolve<IParametersProvider>();
            if (parametersProvider.TryGetParameters(args, out var parameters))
            {
                var tagCloudVisualizer = container.Resolve<ITagCloudVisualizer>();
                var bitmap = tagCloudVisualizer.GetTagCloudBitmap(parameters);
                var resultProcessor = container.Resolve<IResultProcessor>();
                resultProcessor.ProcessResult(bitmap, parameters.OutputFilePath, parameters.ImageFormat);
            }
        }

        private static IContainer GetDependencyInjectionContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<ToInitialFormWordConverter>()
                .As<IWordConverter>();

            var pathToMyStemDirectory =
                Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName, "MyStem");
            builder.RegisterInstance(pathToMyStemDirectory).As<string>();

            return builder.Build();
        }
    }
}
