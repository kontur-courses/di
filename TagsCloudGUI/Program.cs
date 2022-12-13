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
            builder.RegisterType<SettingsProvider>().As<ISettingsProvider>();
            builder.RegisterType<InputFileHandler>().AsSelf();
            builder.RegisterType<WordFilter>().AsSelf();
            var container = builder.Build();
            
            Application.Run(container.Resolve<Form>());
        }
    }
}