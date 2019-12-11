using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Logic;
using TagsCloudVisualization.Logic.Painter;
using TagsCloudVisualization.Services;
using TagsCloudVisualization.UI.Actions;

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
                .Where(t => typeof(IUiAction).IsAssignableFrom(t)).SingleInstance().As<IUiAction>();
            builder.RegisterType<ImageGenerator>().SingleInstance().As<IImageGenerator>();
            builder.RegisterType<TagCloudVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<CircularCloudLayouter>().SingleInstance().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().SingleInstance().As<ICirclePointLocator>();
            builder.RegisterType<TextParser>().SingleInstance().As<IParser>();
            builder.RegisterType<RandomTagPainter>().SingleInstance().As<ITagPainter>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();
            builder.RegisterType<PictureBoxImageHolder>().SingleInstance().As<IImageHolder>().As<PictureBoxImageHolder>();
            builder.RegisterType<AppSettings>()
                .SingleInstance()
                .As<IImageSettingsProvider>()
                .As<IDocumentPathProvider>()
                .As<IBoringWordsProvider>();
            return builder.Build();
        }
    }
}