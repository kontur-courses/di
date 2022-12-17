using System;
using System.IO;
using Autofac;
using CommandLine;
using TagsCloudContainer.App;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static string ProjectPath =>
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args).Value;
            var app = CreateApp(options);
            app.Run();
        }

        private static IApp CreateApp(Options options)
        {
            var builder = new ContainerBuilder();
            var container = builder.ConfigureTagCloud(options);
            return container.Resolve<IApp>();
        }
    }
}