using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            builder.RegisterInstance(new TagsCloudSettings(new Palette(Color.Aqua, Color.Black),
                new ImageSize(500, 500),
                new PossibleFonts(new HashSet<FontStyle> { FontStyle.Regular, FontStyle.Italic, FontStyle.Bold },
                    FontFamily.Families.ToHashSet()), 0.7)).AsSelf();
            builder.RegisterInstance(new RectanglesLayouter(Point.Empty)).As<IRectanglesLayouter>();
            builder.RegisterType<TagsCloudDrawer>().As<ITagsCloudDrawer>();
            builder.RegisterType<TagsCloudHandler>().AsSelf();
            builder.RegisterInstance(new HashSet<string>
            {
                "и",
                "a",
                "в"
            }).AsSelf();
            builder.RegisterType<BlackListWordsFilter>().As<IWordsFilter>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }
    }
}
