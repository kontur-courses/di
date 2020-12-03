using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<TagsCloudUIModule>();

            var container = containerBuilder.Build();
            var parser = container.Resolve<ITextParser>();
            var spiral = container.Resolve<ISpiral>(new NamedParameter("center", new Point(200, 200)),
                new NamedParameter("distanceBetweenLoops", 0.2),
                new NamedParameter("angleDelta", 1.0));
            var cloudLayouter = container.Resolve<ILayouter>(new NamedParameter("spiral", spiral),
                new NamedParameter("center", new Point(200, 200)));

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<TagsCloudForm>(new NamedParameter("parser", parser),
                new NamedParameter("layouter", cloudLayouter)));
        }
    }
}