using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using TagCloudUI.Infrastructure;
using TagCloudUI.Infrastructure.Selectors;
using TagCloudUI.UI;

namespace TagCloudUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var container = BuildContainer(args);
            var ui = container.Resolve<IUserInterface>();
            ui.Run(container.Resolve<AppSettings>());
        }

        private static IContainer BuildContainer(IEnumerable<string> args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleUI>().As<IUserInterface>();

            var dlls = Directory.GetFiles(Environment.CurrentDirectory, "TagCloud*.dll");
            var assemblies = dlls.Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterType<ReaderSelector>().AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<LayoutAlgorithmSelector>().AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<ColoringAlgorithmSelector>().AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AppSettings>().WithParameter("args", args).SingleInstance();
            builder.RegisterType<SpiralFactory>().AsImplementedInterfaces();
            builder.Register(b => b.Resolve<ISpiralFactory>().Create())
                .AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}