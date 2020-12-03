using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloud.App;
using TagsCloud.Infrastructure;
using TagsCloud.UI;

namespace TagsCloud
{
    class Program
    {
        [STAThread]
        private static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(TagsCloudSettings.DefaultSettings).AsSelf();
            builder.RegisterInstance(new RectanglesLayouter(Point.Empty)).As<IRectanglesLayouter>();
            builder.RegisterType<TagsCloudDrawer>().As<ITagsCloudDrawer>();
            builder.RegisterType<TagsCloudHandler>().AsSelf();
            builder.RegisterInstance(new HashSet<string>
            {
                "и",
                "a",
                "в"
            }).AsSelf();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<BlackListWordsFilter>().As<IWordsFilter>();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }
    }
}
