using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;

namespace WindowsFormsApp1
{
    static class Program
    {
        private static IContainer Container { get; set; }

        private static void ContainerConfiguration()
        {
            var builder = new ContainerBuilder();
            builder.Register(x => new CloudConfiguration
            {
                Path = "../../WarAndPeaceWords.txt",
                MinFontSize = 10,
                MaxFontSize = 30,
                WordsInCloud = 50
            });
            builder.Register(x => new IgnoreWordsFiles
            {
                Paths = new[]
                {
                    "../../ignore.txt"
                }
            });
            builder.Register(x => new Point(300, 250));
            builder.RegisterType<TagsCloudVisualizer>();
            builder.RegisterType<CloudCombiner>().As<ICloudCombiner>();
            builder.RegisterType<TxtTextReader>().As<ITextReader>();
            builder.RegisterType<TagStatMaiker>().As<ITagStatMaiker>();
            builder.RegisterType<AllWordsToLowerCase>().As<ITagFilter>();
            builder.RegisterType<IgnoreSpecialWords>().As<ITagFilter>();
            builder.RegisterType<ArchimedeanCircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.RegisterType<WinFormCloudVisualizer>().As<ICloudVisualizer>();

            Container = builder.Build();
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            ContainerConfiguration();

            

            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<TagsCloudVisualizer>().View();
            }
        }
    }
}
