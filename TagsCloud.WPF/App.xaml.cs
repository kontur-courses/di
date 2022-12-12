using System;
using System.Windows;
using Autofac;
using TagsCloud.FileConverter;
using TagsCloud.FileConverter.Implementation;
using TagsCloud.FileReader;
using TagsCloud.FileReader.Implementation;
using TagsCloud.WordHandler;
using TagsCloud.WordHandler.Implementation;
using TagsCloud.WPF.PictureSaver;
using TagsCloud.WPF.PictureSaver.Implementation;

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
            var container = GetContainer("../../../Words.docx");
            var window = container.Resolve<MainWindow>();
            app.Run(window);
        }

        private static IContainer GetContainer(string path)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.Register(_ => new PathShell(path)).As<PathShell>();
            builder.RegisterType<LowerCaseHandler>().As<IWordHandler>();
            builder.RegisterType<BoringRusWordsHandler>().As<IWordHandler>();
            builder.RegisterType<RecurringWordsHandler>().As<IWordHandler>();
            builder.RegisterType<PictureSaverCanvas>().As<IPictureSaver>();
            builder.RegisterType<DocxReader>().As<IFileReader>();
            builder.RegisterType<ConvertToTxt>().As<IFileConverter>();

            return builder.Build();
        }
    }
}