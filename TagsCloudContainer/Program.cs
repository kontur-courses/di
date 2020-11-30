using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ContainerConfig>();
            var container = containerBuilder.Build();

            var parser = container.Resolve<ITextParser>();
            var cloudLayouter = container.Resolve<ILayouter>(new NamedParameter("center", new Point(200, 200)));

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TagsCloudForm(parser, cloudLayouter));
        }
    }
}