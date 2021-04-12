using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
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

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                LoadSettingsProviders(container);

                var pictureBox = container.Get<PictureBoxImageHolder>();
                container.Bind<IImageHolder>().ToConstant(pictureBox);
                container.Bind<PictureBoxImageHolder>().ToConstant(pictureBox);

                container.Bind<Palette>().ToSelf().InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<DragonSettings>().ToFactory();

                BindUiActions(container);

                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void BindUiActions(StandardKernel container)
        {
            container.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IUiAction>() // 2
                .BindAllInterfaces() // 3
                .Configure(b => b.InSingletonScope()));
            //container.Bind<IUiAction>().To<SaveImageAction>();
            //container.Bind<IUiAction>().To<DragonFractalAction>();
            //container.Bind<IUiAction>().To<KochFractalAction>();
            //container.Bind<IUiAction>().To<ImageSettingsAction>();
            //container.Bind<IUiAction>().To<PaletteSettingsAction>();
        }

        private static void LoadSettingsProviders(StandardKernel container)
        {
            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>();
            var settingsManeger = container.Get<SettingsManager>();

            var settings = settingsManeger.Load();
            container.Bind<IImageDirectoryProvider>().ToConstant(settings);
            container.Bind<ImageSettings>()
                .ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings)
                .InSingletonScope();
        }
    }
}