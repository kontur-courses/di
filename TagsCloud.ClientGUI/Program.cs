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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(BuildContainer().Resolve<MainForm>());
        }

        private static IContainer BuildContainer()
        {
            var service = new ContainerBuilder();
            service.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            service.RegisterType<SpiralSettings>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<FontSetting>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<PathSettings>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<Palette>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>()
                .InstancePerLifetimeScope();

            service.RegisterType<SettingsManager>().InstancePerLifetimeScope();
            service.RegisterType<TagsCloudPainter>().InstancePerLifetimeScope();

            service.Register(c => c.Resolve<SettingsManager>().Load()).As<AppSettings>()
                .InstancePerLifetimeScope();
            service.Register(c => c.Resolve<AppSettings>().ImageSettings).As<ImageSettings>()
                .InstancePerLifetimeScope();

            service.RegisterType<MainForm>(); 
            return service.Build();
        }
    }
}