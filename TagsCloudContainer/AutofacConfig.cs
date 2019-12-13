using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using Autofac;
using DocoptNet;
using FluentAssertions.Common;
using TagsCloudContainer.TagCloudVisualization;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer(IDictionary<string, ValueObject> arguments)
        {
            var font = new Font(arguments["--font"].ToString(), 50);
            var backgroundBrush = ArgumentsParser.ParseBrush(arguments["--bgcolor"].ToString());
            var textBrush = ArgumentsParser.ParseBrush(arguments["--textcolor"].ToString());
            var isDebugMode = arguments["--debug"].IsTrue;
            var debugItemRectangleColor = ArgumentsParser.ParseColor(arguments["--dbgrectcolor"].ToString());
            var debugMarkingColor = ArgumentsParser.ParseColor(arguments["--dbgmarkcolor"].ToString());
            var strImgFormat = arguments["--format"].ToString();
            var imgFormat = ArgumentsParser.ParseImageFormat(strImgFormat);

            var builder = new ContainerBuilder();
            builder.Register(c => imgFormat).As<ImageFormat>();
            builder.Register(c => font).As<Font>();
            builder.Register(c => new VisualizationSettings(font, backgroundBrush, textBrush,
                    isDebugMode, debugMarkingColor, debugItemRectangleColor))
                .As<VisualizationSettings>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            var inputSource = arguments["<inputFile>"]?.ToString();
            if (inputSource is null)
            {
                builder.RegisterType<ConsoleReader>().As<IWordProvider>();
            }
            else
            {
                var inputFormat = ResolveInputFormat(inputSource);
                var wordProviderType =
                    executingAssembly.GetTypes()
                        .Where(type => typeof(IWordProvider).IsAssignableFrom(type))
                        .FirstOrDefault(t => t.Name == $"{inputFormat.Capitalize()}Reader");
                builder.RegisterType(wordProviderType ?? throw new ArgumentException("Unsupported input " +
                                                                                     $"file format: {inputFormat}"))
                    .As<IWordProvider>().WithParameter(new TypedParameter(typeof(string), inputSource));
            }

            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(type => !typeof(IWordProvider).IsAssignableFrom(type))
                .AsImplementedInterfaces();

            return builder.Build();
        }

        private static string ResolveInputFormat(string inputSource)
        {
            var a = inputSource.Split(".");
            return a[a.Length - 1];
        }
    }
}