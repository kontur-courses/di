using System;
using System.Collections;
using Ninject;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Parser.Default.ParseArguments(args, option);

            var unsetParams = option.GetUnsetParamtersNames();
            if (unsetParams.Any())
            {
                Console.WriteLine($"{String.Join(", ", unsetParams)} wasn't passed correctly.");
                return;
            }

            var excludingWords = string.IsNullOrWhiteSpace(option.ExcludingFilePath)
                ? new string[0]
                : File.ReadLines(option.ExcludingFilePath);


            var settings = new DrawingSettings(Color.FromName(option.BackColor), Color.FromName(option.TextColor),
                FontFamily.GenericMonospace, new Size((int) option.Width, (int) option.Height), ImageFormat.Png);

            var scope = GetContainer(excludingWords, settings).BeginLifetimeScope();
            var maker = scope.Resolve<ITagCloudMaker>();
            var result = maker.CreateTagCloud(option.InputFilePath, (int)option.FontSize);

            if (!result.IsSuccess)
                Console.WriteLine(result.Error);

            Console.WriteLine(result.GetValueOrThrow());
        }

        static IContainer GetContainer(IEnumerable<string> badWords, DrawingSettings settings)
        {
            var container = new ContainerBuilder();
            container.RegisterInstance(badWords);
            container.Register(_ => Point.Empty);
            container.Register(_ => settings);
            container.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Result))).AsImplementedInterfaces();
            return container.Build();
        }
    }
}
