using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
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
            builder.RegisterType<RectangleVisualisator>().AsSelf();

            builder.Build();
        }
    }
}