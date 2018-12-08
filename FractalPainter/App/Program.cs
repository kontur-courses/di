using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var  kernel = InitDi();
                Application.Run(kernel.Get<Form>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static IKernel InitDi()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Form>().To<MainForm>();
            kernel.Bind<IUiAction>().To<SaveImageAction>();
            kernel.Bind<IUiAction>().To<DragonFractalAction>();
            kernel.Bind<IUiAction>().To<KochFractalAction>();
            kernel.Bind<IUiAction>().To<ImageSettingsAction>();
            kernel.Bind<IUiAction>().To<PaletteSettingsAction>();
            kernel.Bind<IDragonPainterFactory>().ToFactory();
            
            kernel.Bind<IImageHolder, PictureBoxImageHolder>()
                .To<PictureBoxImageHolder>()
                .InSingletonScope();
            kernel.Bind<Palette>().ToSelf().InSingletonScope();
            
            return kernel;
        }
    }
}