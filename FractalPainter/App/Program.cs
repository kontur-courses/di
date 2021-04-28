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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();

                // start here
                // container.Bind<TService>().To<TImplementation>();
                var settingsManager = CreateSettingsManager();
                var imageSettings = settingsManager.Load().ImageSettings;

                var pictureBox = new PictureBoxImageHolder();
                pictureBox.RecreateImage(imageSettings);
                pictureBox.Dock = DockStyle.Fill;

                container.Bind<IImageHolder>().ToConstant(pictureBox);
                container.Bind<SettingsManager>().ToConstant(settingsManager);
                container.Bind<Palette>().ToConstant(new Palette());
                container.Bind<KochPainter>().To<KochPainter>();

                container.Bind<IDragonPainterFactory>().ToFactory();

                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings);

                container.Bind<IImageDirectoryProvider>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load());

                container.Bind<Form>().To<MainForm>();

                container.Bind(x => x.
                    FromThisAssembly()
                    .SelectAllClasses().InNamespaces("FractalPainting.App.Actions")
                    .BindAllInterfaces());
                
                Application.Run(container.Get<Form>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static SettingsManager CreateSettingsManager()
        {
            var container = new StandardKernel();
            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>();
            return container.Get<SettingsManager>();
        }
    }
}