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
            try
            {
                var builder = new ContainerBuilder();
                builder.Register(a => new Palette {BackgroundColor = Color.Black, PrimaryColor = Color.Aqua}).AsSelf();
                builder.Register(a => new ImageSize(500, 500)).AsSelf();
                builder.Register(a => new Font(FontFamily.GenericSansSerif, 25)).AsSelf();
                builder.RegisterType<Mainform>().AsSelf();
                builder.RegisterType<WordsConverter>().As<IWordsConverter>();
                builder.RegisterType<TagcloudSettings>().AsSelf();
                var container = builder.Build();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Resolve<Mainform>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
