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
        private static MainForm mainForm;

        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();

                container.Bind(x => x
                    .FromThisAssembly()
                    .SelectAllClasses().InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                container.Bind<IDragonPainterFactory>().ToFactory();


                container.Bind<Palette>().ToSelf()
                    .InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();


                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();

                container.Bind<AppSettings, IImageDirectoryProvider>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();
                container.Bind<ImageSettings>()
                    .ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();


                mainForm = container.Get<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}