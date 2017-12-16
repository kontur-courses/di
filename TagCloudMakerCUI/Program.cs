﻿using System;
using Ninject;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using Ninject.Parameters;
using TagCloud;
using TagCloud.Implementations;
using TagCloud.Interfaces;
using CommandLine;
using CommandLine.Text;

namespace TagCloudMakerCUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var option = new Option();
            var isValid = Parser.Default.ParseArguments(args, option);
            var unsetParams = option.GetUnsetParamtersNames();
            if (!isValid || unsetParams.Any())
            {
                Console.WriteLine($"{String.Join(", ", unsetParams)} wasn't passed correctly.");
                return;
            }

            var excludingWords = string.IsNullOrWhiteSpace(option.ExcludingFilePath)
                ? new string[0]
                : File.ReadLines(option.ExcludingFilePath);
            using (var scope = GetContainer(excludingWords).BeginLifetimeScope())
            {
                var maker = scope.Resolve<ITagCloudMaker>();
                var path = maker.CreateTagCloud(option.InputFilePath, (int)option.FontSize,
                    new DrawingSettings(Color.FromName(option.BackColor), Color.FromName(option.TextColor), 
                    FontFamily.GenericMonospace, new Size((int)option.Width, (int)option.Height), ImageFormat.Png));
                Console.WriteLine(path);
            }
        }

        static IContainer GetContainer(IEnumerable<string> badWords)
        {
            var container = new ContainerBuilder();
            container.RegisterType<MystemShell>().As<IMystemShell>();
            container.RegisterType<WordProcessor>().As<IWordProcessor>().WithParameter("badWords", badWords);
            container.RegisterType<SpiralPointComputer>().As<IPointComputer>().WithParameter("center", new Point(0, 0));
            container.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            container.RegisterType<TagCloudDrawer>().As<ITagCloudDrawer>();
            container.RegisterType<ImageSaver>().As<IImageSaver>();
            container.RegisterType<TagCloudMaker>().As<ITagCloudMaker>();
            return container.Build();
        }
    }
}
