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
    public class Program
    {
        public static void Main(string[] args)
        {
            var userInterface = new ConsoleUserInterface();
            if (userInterface.TryGetParameters(args, out var parameters))
            {
                var container = GetDependencyInjectionContainer(parameters, userInterface);
                var tagCloudVisualizer = container.Resolve<ITagCloudVisualizer>();
                var bitmap = tagCloudVisualizer.GetTagCloudBitmap(parameters);
                var resultProcessor = container.Resolve<IResultProcessor>();
                resultProcessor.ProcessResult(bitmap, parameters.OutputFilePath);
            }
        }

        private static IContainer GetDependencyInjectionContainer(Parameters parameters, IUserInterface userInterface)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterInstance(userInterface)
                .As<IResultDisplay>();

            var pathToMyStemDirectory = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName,
                "WordProcessing", "Filtering", "MyStem");
            builder.RegisterInstance(pathToMyStemDirectory).As<string>();

            builder.RegisterInstance(parameters)
                .As<Parameters>();
            builder.Register(c => c.Resolve<Parameters>().Colors)
                .As<List<Color>>();

            return builder.Build();
        }
    }
}
