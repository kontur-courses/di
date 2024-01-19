using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
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
                
                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();

                container.Bind(x => x
                    .FromThisAssembly()
                    .SelectAllClasses().InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();

                container.Bind<IImageDirectoryProvider, AppSettings>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings).InSingletonScope();
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