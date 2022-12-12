using System;
using System.Drawing;
using System.IO;
using Autofac;
using TagsCloudContainer.Application;
using TagsCloudContainer.TextReaders;
using TagsCloudContainer.Visualisators;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Container
{
    public static class Container
    {
        public static IContainer SetDiBuilder(Options options)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var builder = new ContainerBuilder();
            builder.Register(x => new Settings()
                {
                    WordColor = Color.FromName(options.ColorName),
                    WordFontName = options.FontName,
                    WordFontSize = options.FontSize,
                    FileName = options.InputFile,
                    BoringWordsFileName = options.BorringWordsFile
                })
                .As<Settings>();
            if (options.InputFile.Contains("docx"))
                builder.RegisterType<WordReader>().As<ITextReader>();
            else
                builder.RegisterType<TxtReader>().As<ITextReader>();
            builder.RegisterType<WordHandler>().AsSelf();
            builder.Register(x =>
                    new CircularCloudLayouter(new Point(options.CenterX, options.CenterY)))
                .As<CircularCloudLayouter>();
            builder.RegisterType<RectangleVisualisator>().As<IVisualisator>();
            builder.RegisterType<ConsoleApp>().As<IApp>();

            return builder.Build();
        }
    }
}