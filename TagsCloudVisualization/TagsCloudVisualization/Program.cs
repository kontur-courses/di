using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;

namespace TagsCloudVisualization
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var builder = new ContainerBuilder();
                builder.Register(p => new Point(300, 300)).AsSelf();
                builder.RegisterType<TxtReader>().As<IFileReader>();
                builder.RegisterType<LinearSizer>().As<ISizeDefiner>();
                builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                builder.RegisterType<Visualizer>().As<IVisualizer>();
                builder.RegisterType<MainForm>().AsSelf();
                builder.Register(p => new MonochromePalette(new Font("Arial", 10), Color.Aquamarine, Color.Black)).As<IWordPalette>();
                var container = builder.Build();
                Application.Run(container.Resolve<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
