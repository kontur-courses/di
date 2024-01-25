﻿using Autofac;
using CommandLine;
using TagsCloudContainer.App;
using TagsCloudContainer.App.Interfaces;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var container = Container.SetDiBuilder(options);
            var app = container.Resolve<IApp>();
            app.Run();
        }
    }
}