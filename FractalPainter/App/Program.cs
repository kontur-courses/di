using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;

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
            var kernel = new StandardKernel();
            try
            {
                SetUpDependencies(kernel);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(kernel.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void SetUpDependencies(StandardKernel kernel)
        {
            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IUiAction>()
                .BindAllInterfaces());

            kernel.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
            kernel.Bind<Palette>().ToSelf().InSingletonScope();

            kernel.Bind<IDragonPainterFactory>().ToFactory();

            kernel.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
            kernel.Bind<IBlobStorage>().To<FileBlobStorage>();

            kernel.Bind<IImageSettingsProvider, IImageDirectoryProvider>()
                .ToMethod(ctx => ctx.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
            kernel.Bind<ImageSettings>().ToMethod(ctx => ctx.Kernel.Get<IImageSettingsProvider>().ImageSettings)
                .InSingletonScope();
        }
    }
}
