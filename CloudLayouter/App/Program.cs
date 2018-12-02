using System;
using System.Windows.Forms;
using Autofac;
using CloudLayouter.Actions;
using CloudLayouter.Infrastructer;
using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                #region Dependencies

                var builder = new ContainerBuilder();

                builder.RegisterType(typeof(MainForm));
                builder.RegisterType<ImageSettings>().SingleInstance();
                builder.RegisterType<PictureBoxImageHolder>().SingleInstance();
                builder.RegisterType<SaveImageAction>().As<IUiAction>();
                builder.RegisterType<IImageDirectoryProvider>();
                builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder>();
                builder.RegisterType<ImageDirectoryProvider>().As<IImageDirectoryProvider>();
                var container = builder.Build();

                #endregion

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Resolve<MainForm>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}