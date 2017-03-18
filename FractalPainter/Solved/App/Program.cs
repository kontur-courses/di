using System;
using System.Windows.Forms;
using FractalPainting.Solved.App.Fractals;
using FractalPainting.Infrastructure;
using FractalPainting.Solved.App.Actions;
using Ninject;
using Ninject.Extensions.Conventions;

namespace FractalPainting.Solved.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new StandardKernel();
            
            container.Bind(kernel => kernel
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IUiAction>()
                .BindAllInterfaces());

            container.Bind<Palette>().ToConstant(new Palette())
                .InSingletonScope();
            container.Bind<IImageHolder, PictureBoxImageHolder>()
                .To<PictureBoxImageHolder>()
                .InSingletonScope();

            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                .WhenInjectedInto<SettingsManager>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>()
                .WhenInjectedInto<SettingsManager>();

            container.Bind<IImageDirectoryProvider, AppSettings>()
                .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                .InSingletonScope();
            container.Bind<ImageSettings>()
                .ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings);

            container.Bind<Random>().To<Random>().WhenInjectedInto<DragonSettingsGenerator>().InThreadScope();
            container.Bind<IDragonSettingsGenerator>().To<DragonSettingsGenerator>();

            try
            {
                var form = container.Get<MainForm>();
                Application.Run(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
		}
	}
}