using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
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
                var container = new StandardKernel();

                // start here
                container.Bind(c => c.FromThisAssembly().SelectAllClasses().BindAllInterfaces());
                container.Bind(c => c.FromThisAssembly().SelectAllClasses().BindAllBaseClasses());
                container.Bind<IImageHolder>().To<PictureBoxImageHolder>().InSingletonScope().Named("single");

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