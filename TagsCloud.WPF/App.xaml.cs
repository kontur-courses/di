using System;
using System.Windows;
using Ninject;
using TagsCloud.FontFinder;
using TagsCloud.FontFinder.Implementation;
using TagsCloud.WordHandler;
using TagsCloud.WordHandler.Implementation;

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
            var container = GetBuilder();
            var window = container.Get<MainWindow>();
            app.Run(window);
        }

        private static IKernel GetBuilder()
        {
            var container = new StandardKernel();
            container.Bind<IFontFinder>().To<WindowsFontFinder>();
            container.Bind<IWordHandler>().To<LowerCaseHandler>();
            container.Bind<IWordHandler>().To<BoringRusWordsHandler>();
            container.Bind<IWordHandler>().To<RecurringWordsHandler>().InSingletonScope();
            
            return container;
        }
    }
}