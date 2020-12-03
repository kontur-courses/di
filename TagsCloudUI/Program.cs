using System;
using System.IO;
using System.Windows.Forms;
using Autofac;

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

            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var path = Path.Join(root, "TagsCloudContainer", "Texts", "ParsedSong.txt");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                container.Resolve<TagsCloudForm>(new TypedParameter(typeof(string), File.ReadAllText(path))));
        }
    }
}