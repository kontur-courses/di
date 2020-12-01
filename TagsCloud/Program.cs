using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using Autofac;
using TagsCloud;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.Register(a => new ImageSize(500, 500)).AsSelf();
            builder.Register(a => new Font(FontFamily.GenericSansSerif, 25)).AsSelf();
            builder.Register(a => new Palette(Color.Aqua, Color.Black)).AsSelf();
            builder.Register(a => new RectanglesConstellator(Point.Empty)).As<IRectanglesConstellator>();
            builder.RegisterType<TagscloudDrawer>().As<ITagscloudDrawer>();
            builder.RegisterType<TagscloudHandler>().AsSelf();
            builder.Register(a => new HashSet<string>
            {
                "и",
                "a",
                "в"
            }).AsSelf();
            builder.RegisterType<WordsConverter>().As<IWordsConverter>();
            builder.RegisterType<TagcloudSettings>().AsSelf();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }
    }
}
