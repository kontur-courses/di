using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using TagsCloud.CloudStructure;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.WordPrework;
using CommandLine;

namespace TagsCloud
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = Options.Parse(args);
            var container = Ioc.Configure(options);
            var app = container.Resolve<Application>();
            app.Run(options);
        }
    }
}
