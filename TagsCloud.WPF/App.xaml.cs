using System;
using System.Windows;
using Autofac;
using TagsCloud.WPF.ContainerConfigurator;
using TagsCloud.WPF.ContainerConfigurator.Implementation;

namespace TagsCloud.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            InitializeComponent();
        }
 
        [STAThread]
        private static void Main()
        {
            var app = new App();
            var container = GetContainer(new WpfContainerDocx());
            var window = container.Resolve<MainWindow>();
            app.Run(window);
        }

        private static IContainer GetContainer(IContainerConfigurator config) => config.GetContainer();
    }
}