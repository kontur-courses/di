using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization;

namespace TagCloudGraphicalUserInterface
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var builder = new ContainerBuilder();
            builder.RegisterType<TagAction>().As<IActionForm>();
            builder.RegisterTypes(typeof(Dictionary<string, int>), typeof(CircularCloudLayouter), typeof(ArithmeticSpiral), typeof(TagCloud), typeof(List<TextRectangle>), typeof(List<Point>),
                typeof(AppSettings), typeof(Palette), typeof(CloudForm), typeof(ImageSettings)).AsSelf();
            builder.RegisterType<PictureBoxTags>().As<IImage,PictureBoxTags>().SingleInstance();
            builder.Register(x=>new Point(0,0));
            var container = builder.Build();
            Application.Run(container.Resolve<CloudForm>());
        }
    }
}
