using System;
using System.Windows.Forms;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;
using FractalPainting.Infrastructure.UiActions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.App.Fractals;
using FractalPainting.App.Actions;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = CreateContainer();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static StandardKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind<IDragonPainterFactory>().ToFactory();
            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                .WhenInjectedInto<SettingsManager>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>()
                .WhenInjectedInto<SettingsManager>();
            container.Bind<SettingsManager>().ToSelf();

            container.Bind(config
                => config.FromThisAssembly().SelectAllClasses()
                .InheritedFrom<IUiAction>().BindAllInterfaces());

            container.Bind<AppSettings>().ToMethod(context
            => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
            container.Bind<ImageSettings>().ToMethod(context
                => context.Kernel.Get<AppSettings>().ImageSettings).InSingletonScope();
            container.Bind<IImageHolder, PictureBoxImageHolder>()
                .To<PictureBoxImageHolder>().InSingletonScope();
            container.Bind<IImageDirectoryProvider>().To<AppSettings>().InSingletonScope();
            container.Bind<Palette>().ToSelf().InSingletonScope();
            container.Bind<KochPainter>().ToSelf();
            return container;
        }
    }
}