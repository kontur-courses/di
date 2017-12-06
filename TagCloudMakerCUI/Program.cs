using System;
using Ninject;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using Ninject.Parameters;
using TagCloud;
using TagCloud.Implementations;
using TagCloud.Interfaces;

namespace TagCloudMakerCUI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var scope = GetContainer(new [] {"qwertyuiop"}).BeginLifetimeScope())
            {
                var maker = scope.Resolve<ITagCloudMaker>();
                var path = maker.CreateTagCloud("in.txt", 10,
                    new DrawingSettings(Color.White, Color.Black, FontFamily.GenericMonospace,
                        new Size(1000, 500), ImageFormat.Png));
                Console.WriteLine(path);
            }
        }

        static IContainer GetContainer(IEnumerable<string> badWords)
        {
            var container = new ContainerBuilder();
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
