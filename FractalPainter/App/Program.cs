using System;
using System.Linq;
using System.Windows.Forms;
using Ninject;
using Ninject.Extensions.Factory;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

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
                // container.Bind<TService>().To<TImplementation>();
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();
                
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<Func<Random, DragonSettingsGenerator>>().ToFactory();
                
                container.Bind<Form>().To<MainForm>();
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}