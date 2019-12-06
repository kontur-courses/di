using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Actions;

namespace TagsCloudVisualization
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var container = InitializeContainer();
            Application.Run(container.Resolve<MainForm>());
        }

        public static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IUiAction).IsAssignableFrom(t)).As<IUiAction>();

            builder.RegisterType<TagCloudVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<CircularCloudLayouter>().SingleInstance().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().SingleInstance().As<ICirclePointLocator>();
            builder.RegisterType<TextParser>().SingleInstance().As<IParser>();
            builder.RegisterType<RandomTagPainter>().SingleInstance().As<ITagPainter>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();
            builder.RegisterType<PictureBox>().SingleInstance().As<PictureBox>();
            return builder.Build();
        }
    }
}