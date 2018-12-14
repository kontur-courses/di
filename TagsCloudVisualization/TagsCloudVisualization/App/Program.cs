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
                builder.Register(a => args).AsSelf();
                builder.RegisterType<ConsoleCloudGenerator>().As<ICloudGenerator>();
                builder.RegisterType<TxtReader>().As<IFileReader>();
                builder.RegisterType<NWordSizer>().As<ISizeDefiner>();
                builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                builder.RegisterType<Visualizer>().As<IVisualizer>();
                builder.Register(p => new ImageSettings {Center = new Point(500, 500), Size = new Size(1000, 1000)}).As<IImageSettings>();
                builder.Register(p => new MonochromePalette(Color.Black, Color.White)).As<IWordPalette>();
                var container = builder.Build();

                var generator = container.Resolve<ICloudGenerator>();
                if (generator is MainForm form)
                    Application.Run(form);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
