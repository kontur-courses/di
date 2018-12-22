using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using WordCloud.CloudControl;
using WordCloudImageGenerator;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.LayoutCraetion.Layouters.Circular;
using WordCloudImageGenerator.Parsing.BlackList;
using WordCloudImageGenerator.Parsing.Extractors;

namespace WordCloud
{
    static class Program
    {
        private static IContainer Container { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigureContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var scope = Container.BeginLifetimeScope())
            {
                var form = scope.Resolve<TagClodForm>();
                Application.Run(form);
            }
        }

        private static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CommonWords>().As<IBlackList>();
            builder.RegisterType<SimpleExtractor>().As<IWordExtractor>();
            builder.RegisterType<Vizualizer>().AsSelf();

            builder.RegisterType<WordCloudConfig>().AsSelf();
            builder.RegisterType<TagClodForm>().AsSelf();
            builder.Register(_=>LayoutTypes.Circular).As<LayoutTypes>();

            var palette = new List<Brush>()
            {
                Brushes.CadetBlue,
                Brushes.DeepPink,
                Brushes.Coral,
                Brushes.GreenYellow,
                Brushes.Green,
            };

            builder.RegisterInstance(palette).As<IEnumerable<Brush>>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<Vizualizer>().As<ITagCloudVizualizer>();
            builder.Register(_ => new Point(0, 0)).As<Point>();
            Container = builder.Build();
        }
    }
}
