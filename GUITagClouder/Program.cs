using System;
using System.Windows.Forms;
using ConsoleTagClouder;
using TagCloud;
using Autofac;

namespace GUITagClouder
{
    internal class Program
    {
        [STAThreadAttribute]
        public static void Main(string[] args)
        {
            var container = new ContainerBuilder();
            container.RegisterType<NewFileAction>().As<IGuiAction>();
            container.RegisterType<SaveImageAction>().As<IGuiAction>();
            container.RegisterType<DrawingSettingsAction>().As<IGuiAction>();
            container.RegisterType<ClouderSettingsAction>().As<IGuiAction>();
            container.RegisterType<CloudImageHolder>().As<IImageHolder, CloudImageHolder>().SingleInstance();
            container.RegisterType<PathProvider>().As<IPathProvider>().SingleInstance();
            container.Register(c=>CloudSettings.Default()).AsSelf();
            container.Register(c=>DrawingSettings.Default()).AsSelf();
            container.RegisterType<MainForm>().AsSelf();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(container.Build().Resolve<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}