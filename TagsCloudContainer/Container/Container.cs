using System;
using System.Drawing;
using System.IO;
using Autofac;
using TagsCloudContainer.Application;

namespace TagsCloudContainer
{
    public static class Container
    {
        public static IContainer SetDIBuilder()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var builder = new ContainerBuilder();
            builder.Register(x => new Settings()
                {
                    WordColor = Color.Purple,
                    WordFontName = "Arial",
                    WordFontSize = 16
                })
                .As<Settings>();
            builder.Register(x =>
                    new WordHandler($"{projectDirectory}\\TextFiles\\Example.txt"))
                .As<WordHandler>();
            builder.Register(x => new CircularCloudLayouter(new Point()))
                .As<CircularCloudLayouter>();
            builder.RegisterType<RectangleVisualisator>().As<IVisualisator>();
            builder.RegisterType<ConsoleApp>().As<IApp>();
            
            return builder.Build();
        }
    }
}