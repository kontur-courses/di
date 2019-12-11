using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.Core;
using TagsCloudContainer.ResultProcessing;
using TagsCloudContainer.UserInterface;
using TagsCloudContainer.UserInterface.Window;
using TagsCloudContainer.WordProcessing.Converting;

namespace TagsCloudContainer
{
    public class Program
    {
        private static readonly IContainer Container = GetDependencyInjectionContainer();

        [STAThread]
        public static void Main(string[] args)
        {
            var userInterface = Container.Resolve<IUserInterface>();
            userInterface.Run(args, RunProgram);
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
            builder.RegisterType<ApplicationForm>()
                .As<IUserInterface>();
            builder.RegisterType<ApplicationForm>()
                .As<IResultDisplay>();
            builder.RegisterType<ApplicationForm>()
                .As<Form>();

            var pathToMyStemDirectory =
                Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName, "MyStem");
            builder.RegisterInstance(pathToMyStemDirectory).As<string>();

            return builder.Build();
        }

        private static void RunProgram(Parameters parameters)
        {
            var tagCloudVisualizer = Container.Resolve<ITagCloudVisualizer>();
            var bitmap = tagCloudVisualizer.GetTagCloudBitmap(parameters);
            var resultProcessor = Container.Resolve<IResultProcessor>();
            resultProcessor.ProcessResult(bitmap, parameters.OutputFilePath, parameters.ImageFormat);
        }
    }
}
