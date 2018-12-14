using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using WordCloud.LayoutGeneration.Layoter;
using WordCloud.TextAnalyze;
using WordCloud.TextAnalyze.Extractors;

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
            builder.RegisterType<TagClodForm>().AsSelf();

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
            builder.RegisterType<CloudControl>().AsSelf();
            builder.Register(_ => new Point(0, 0)).As<Point>();
            Container = builder.Build();
        }
    }
}
