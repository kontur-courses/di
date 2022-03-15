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

                // start here
                container.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom<IUiAction>().BindAllInterfaces().Configure(b => b.InSingletonScope()));

                container.Bind<IImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();               
                container.Bind<Palette>().ToSelf();
                container.Bind<IPainter>().To<KochPainter>();
                
                container.Bind<MainForm>().ToSelf().InSingletonScope();
                container.Bind<IDragonPainterFactory>().ToFactory();

                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>().WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>().WhenInjectedInto<SettingsManager>();
                container.Bind<AppSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings).InSingletonScope();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);             
                var frm = container.Get<MainForm>();
                Application.Run(frm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}