using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.App;

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
                builder.RegisterType<ConsoleApplication>().As<IApplication>();
                builder.RegisterType<ConsoleApplicationRunner>().As<IApplicationRunner>();
                builder.RegisterType<TxtReader>().As<IFileReader>();
                builder.RegisterType<NWordSizer>().As<ISizeDefiner>();
                builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                builder.RegisterType<Visualizer>().As<IVisualizer>();
                builder.RegisterType<ImageSettings>().As<IImageSettings>();
                builder.RegisterType<MonochromePalette>().As<IWordPalette>();
                var container = builder.Build();

                var generator = container.Resolve<IApplication>();
                var runner = container.Resolve<IApplicationRunner>();

                runner.Run(generator, args);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
