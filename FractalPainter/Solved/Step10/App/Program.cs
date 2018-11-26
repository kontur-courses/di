using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step10.App.Fractals;
using FractalPainting.Solved.Step10.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.Solved.Step10.App
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

                container.Bind(kernel => kernel
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                container.Bind<Palette>().ToSelf()
                    .InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();

                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>()
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<AppSettings, IImageDirectoryProvider>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();
                container.Bind<ImageSettings>()
                    .ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();

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