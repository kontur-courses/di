using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
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
                container.Bind<MainForm>().ToSelf().InSingletonScope();
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();

                
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<SettingsManager>().ToSelf();
                container.Bind<ImageSettings>().ToMethod(ctx => ctx.Kernel.Get<SettingsManager>().Load().ImageSettings);
                
                container.Bind<PictureBoxImageHolder>().ToMethod(ctx =>
                {
                    var imageSettings = ctx.Kernel.Get<SettingsManager>().Load().ImageSettings;
                    var pictureBox = new PictureBoxImageHolder();
                    pictureBox.RecreateImage(imageSettings);
                    pictureBox.Dock = DockStyle.Fill;

                    return pictureBox;
                }).InSingletonScope();

                container.Bind<Palette>().ToSelf().InSingletonScope();
                
                container.Bind<IImageHolder>().ToMethod(ctx => ctx.Kernel.Get<PictureBoxImageHolder>());
                container.Bind<IDragonPainterFactory>().ToFactory();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = container.Get<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}