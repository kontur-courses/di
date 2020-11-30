using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var service = new ContainerBuilder();
            service.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            service.RegisterType<Palette>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>()
                .InstancePerLifetimeScope();

            service.RegisterType<SettingsManager>().InstancePerLifetimeScope();
            service.RegisterType<TagsCloudPainter>().InstancePerLifetimeScope();
            service.RegisterType<TagsCreator>().InstancePerLifetimeScope();

            service.Register(c => c.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>()
                .InstancePerLifetimeScope();
            service.Register(c => c.Resolve<AppSettings>().ImageSettings).As<ImageSettings>()
                .InstancePerLifetimeScope();

            service.RegisterType<MainForm>();
            var app = service.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(app.Resolve<MainForm>());
        }
    }
}