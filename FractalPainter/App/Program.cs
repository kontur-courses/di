using System;
using System.Windows.Forms;
using Ninject;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.UiActions;
using FractalPainting.Infrastructure.Common;

using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

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
                var container = new StandardKernel();

                // start here

                container.Bind(context => context.FromThisAssembly());
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();

                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().To<Palette>().InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<AppSettings, IImageDirectoryProvider>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load());
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings).InSingletonScope();

                

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}