using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud.Settings;
using TagCloud.TagCloudVisualization.Layouter;
using TagCloud.Words;

namespace TagCloud
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Program))).AsImplementedInterfaces().SingleInstance();
                builder.RegisterType<CircularCloudLayouter>().AsSelf();
                builder.RegisterType<ImageBox>().AsSelf().SingleInstance();
                builder.RegisterType<ApplicationWindow>().AsSelf();
                builder.RegisterType<TagCloudVisualization.Visualization.TagCloudVisualization>().AsSelf();
                builder.RegisterInstance(new ImageSettings()).AsSelf().SingleInstance();
                builder.RegisterInstance(new FontSettings()).AsSelf().SingleInstance();
                var mainForm = builder.Build().ResolveOptional<ApplicationWindow>();
                Application.Run(mainForm);

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
}

}