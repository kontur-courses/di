using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var builder = new ContainerBuilder();
            builder.RegisterType<MainForm>().As<Form>();
            builder.RegisterType<DefaultDrawer>().As<IDrawer>();
            builder.RegisterType<InputFileHandler>().As<IInputTextProvider>().SingleInstance();
            builder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance();
            builder.RegisterType<DefaultRectangleArranger>().As<IRectangleArranger>();
            builder.RegisterType<WordFilter>().As<IWordFilter>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            var container = builder.Build();
            
            Application.Run(container.Resolve<Form>());
        }
    }
}