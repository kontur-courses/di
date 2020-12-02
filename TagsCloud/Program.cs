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
        private static void Main()
        {
            var builder = new ContainerBuilder();
            RegisterSettings(builder);
            builder.RegisterInstance(new RectanglesLayouter(Point.Empty)).As<IRectanglesLayouter>();
            builder.RegisterType<TagsCloudDrawer>().As<ITagsCloudDrawer>();
            builder.RegisterType<TagsCloudHandler>().AsSelf();
            builder.RegisterInstance(new HashSet<string>
            {
                "и",
                "a",
                "в"
            }).AsSelf();
            builder.RegisterType<WordsConverter>().As<IWordsConverter>();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }

        private static void RegisterSettings(ContainerBuilder builder)
        {
            builder.RegisterType<TagsCloudSettings>().AsSelf();
            builder.RegisterInstance(new ImageSize(500, 500)).AsSelf();
            builder.RegisterInstance(new Palette(Color.Aqua, Color.Black)).AsSelf();
            builder.RegisterType<PossibleFonts>().AsSelf();
            builder.RegisterInstance(FontFamily.Families.ToHashSet()).AsSelf();
            builder.RegisterInstance(new HashSet<FontStyle> {FontStyle.Regular, FontStyle.Italic, FontStyle.Bold}).AsSelf();
        }
    }
}
