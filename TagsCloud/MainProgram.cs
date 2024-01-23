
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloud.Actions;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;
using TagsCloud.Settings;

namespace TagsCloud;

public class MainProgram
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => typeof(IUiAction).IsAssignableFrom(t))
            .AsImplementedInterfaces();
        containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => typeof(IParser).IsAssignableFrom(t))
            .AsImplementedInterfaces();
        containerBuilder.RegisterType<FileReader>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<CloudForm>();
        containerBuilder.RegisterType<TagCloudPainter>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
        containerBuilder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<AppSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<TagSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<WordAnalyzerSettings>().AsSelf().SingleInstance();
        var cloudForm = containerBuilder.Build().Resolve<CloudForm>();
        Application.Run(cloudForm);
    }
}