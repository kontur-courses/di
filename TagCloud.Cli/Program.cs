using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;
using TagCloud;
using TagCloud.Enums;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;
using Point = TagCloud.Layouter.Point;
using Size = System.Drawing.Size;

namespace TagCloudCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = Configuration.FromArguments(args);
            var container = ContainerBuilder.ConfigureContainer(configuration);
            var app = container.Resolve<Application>();
            app.Run(configuration.InputFile, configuration.OutputFile);
        }
    }
}