using System;
using System.Drawing;
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
                builder.RegisterType<TxtReader>().As<IFileReader>();
                builder.RegisterType<NWordSizer>().As<ISizeDefiner>();
                builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                builder.RegisterType<Visualizer>().As<IVisualizer>();
                builder.RegisterType<MainForm>().AsSelf();
                builder.Register(p => new ImageSettings {Center = new Point(500, 500), Size = new Size(1000, 1000)}).As<IImageSettings>();
                builder.Register(p => new MonochromePalette(Color.Black, Color.White)).As<IWordPalette>();
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
