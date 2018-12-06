using System;
using System.Windows.Forms;
using Autofac;
using CloudLayouter.Actions;
using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Common.Settings;
using CloudLayouter.Infrastructer.Interfaces;
using CloudLayouter.Painters;

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
                builder.RegisterType<MainForm>();
                builder.RegisterType<TagCloudPainter>();
                builder.RegisterType<ImageSettings>().SingleInstance();
                builder.RegisterType<Palette>().SingleInstance();
                builder.RegisterType<TagSettings>().SingleInstance();
                builder.RegisterType<SaveImageAction>().As<IUiAction>().SingleInstance();
                builder.RegisterType<TagSettingsAction>().As<IUiAction>().SingleInstance();
                builder.RegisterType<ImageSettingsAction>().As<IUiAction>().SingleInstance();
                builder.RegisterType<PaletteSettingsAction>().As<IUiAction>().SingleInstance();
                builder.RegisterType<TagLayoutAction>().As<IUiAction>().SingleInstance();

                builder.RegisterType<PictureBoxImageHolder>()
                    .As(typeof(PictureBoxImageHolder), typeof(IImageHolder)).SingleInstance();
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