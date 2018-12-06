using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using Autofac;
using CommandLine;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class Options
    {
        [Value(0, MetaName = "filename", HelpText = "Text file", Required = true)]
        public string Filename { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var source = new PreparedFile(options.Filename);

                var builder = new ContainerBuilder();
                //builder.RegisterType<ConsoleUI>().As<IUserInterface>();
                builder.RegisterType<WordsPreprocessor>().As<IWordsPreprocessor>();
                builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>();
                builder.RegisterInstance(source).As<ISource>();
                
                var container = builder.Build();
                //var client = container.Resolve<IUserInterface>();
                var client = container.Resolve<ICloudVisualizer>();

                client.VisualizeCloud();
            });
        }
    }
}
