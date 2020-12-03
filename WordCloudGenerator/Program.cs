using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using Autofac.Core;

namespace WordCloudGenerator
{
    public static class Program
    {
        public static void Main()
        {
            var container = Configure();
            container.Resolve<IUserInterface>().Run(container);
        }

        private static IContainer Configure()
        {
            var paletteColors = new[]
            {
                Color.Indigo, Color.Brown, Color.BlueViolet, Color.ForestGreen,
                Color.Black, Color.Khaki,  Color.Teal
            };
            
            var builder = new ContainerBuilder();
            builder.RegisterType<Preparer>().WithParameter("filter", new Func<string, bool>(str => str.Length >= 3));
            builder.RegisterType<Generator>();
            builder.RegisterType<Painter>();
            builder.RegisterInstance(FontFamily.GenericSansSerif).As<FontFamily>();

            builder.RegisterType<RandomChoicePalette>().As<IPalette>().WithParameters(new[]
                {new NamedParameter("colors", paletteColors), new NamedParameter("backgroundColor", Color.Bisque)});
            builder.RegisterInstance(new CircularLayouter(new Point(0,0))).As<ILayouter>();
            builder.RegisterType<ConsoleUI>().As<IUserInterface>();


            return builder.Build();
        }
    }
}