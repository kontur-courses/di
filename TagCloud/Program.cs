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
                builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Program))).AsImplementedInterfaces();
                builder.RegisterType<TagGenerator>().AsSelf().SingleInstance();
                builder.RegisterType<ExcludingWords>().AsSelf().SingleInstance();
                builder.RegisterType<CircularCloudLayouter>().AsSelf().SingleInstance();
                builder.RegisterType<TagCloudLayouter>().AsSelf().SingleInstance();
                builder.RegisterType<Words.Words>().AsSelf().SingleInstance();
                builder.RegisterType<ImageBox>().AsSelf().SingleInstance();
                builder.RegisterType<ApplicationWindow>().AsSelf();
                builder.RegisterType<WordFilter>().AsSelf();
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