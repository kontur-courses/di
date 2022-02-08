using System;
using System.Windows.Forms;
using FractalPainting.App.Fractals;
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
            try
            {
                var container = new StandardKernel();

                // start here
                container.Bind(x =>
                    x.FromThisAssembly().SelectAllClasses().InheritedFrom<IUiAction>().BindAllInterfaces());
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<SettingsManager>().ToSelf().InSingletonScope();
                container.Bind<PictureBoxImageHolder>().ToSelf().InSingletonScope();
                container.Bind<IImageHolder>().ToMethod(context => context.Kernel.Get<PictureBoxImageHolder>());
                container.Bind<ImageSettings>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings);
                container.Bind<IImageDirectoryProvider>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load());
                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<DragonSettingsGenerator>()
                    .ToMethod(context => new DragonSettingsGenerator(new Random()));
                container.Bind<Palette>().ToSelf();

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