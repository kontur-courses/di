using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
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
        ///         public MainForm()
    //    : this(
    //new IUiAction[]
    //{
    //    new SaveImageAction(),
    //    new DragonFractalAction(),
    //    new KochFractalAction(),
    //    new ImageSettingsAction(),
    //    new PaletteSettingsAction()
    //})
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();
                container.Bind(kernel => kernel
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces());
                container.Bind<IImageHolder,PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<KochPainter>().ToSelf().InSingletonScope();
                container.Bind<DragonPainter>().ToSelf().InSingletonScope();
                container.Bind<IDragonFactory>().ToFactory();
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>().WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>().WhenInjectedInto<SettingsManager>();
                container.Bind<AppSettings, IImageDirectoryProvider>()
                    .ToMethod(ctx => ctx.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();
                container.Bind<ImageSettings>()
                    .ToMethod(ctx => ctx.Kernel.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();
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