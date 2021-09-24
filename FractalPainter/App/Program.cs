using System;
using System.Windows.Forms;
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
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                
                container.Bind(x => x.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindSingleInterface());
                
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<ImageSettings>().ToMethod(ctx => ctx.Kernel.Get<SettingsManager>().Load().ImageSettings);
                container.Bind<IImageDirectoryProvider>().ToMethod(ctx => ctx.Kernel.Get<SettingsManager>().Load());
                
                container.Bind<PictureBoxImageHolder>().ToSelf().InSingletonScope();

                container.Bind<Palette>().ToSelf().InSingletonScope();
                
                container.Bind<IImageHolder>().ToMethod(ctx => ctx.Kernel.Get<PictureBoxImageHolder>());
                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<DragonSettings>().ToMethod(ctx => new DragonSettingsGenerator(new Random()).Generate());

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