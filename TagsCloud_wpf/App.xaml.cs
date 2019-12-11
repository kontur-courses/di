using Autofac;
using System.Windows;
using TagsCloud.DI;

namespace TagsCloud_wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule());
            builder.RegisterType<MainWindow>().AsSelf();  

            var container = builder.Build();
            var window = container.Resolve<MainWindow>();
            window.Show();            
        }
    }
}
