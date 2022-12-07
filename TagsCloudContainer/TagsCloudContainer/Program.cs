using Autofac;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Parser;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainForm>().As<Form>();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FileSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ImageHolder>().AsSelf().SingleInstance();
            builder.RegisterType<Parser.Parser>().AsSelf();
            builder.RegisterType<ChoseSourceFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<AlgorithmSettingsAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<DrawImageAction>().As<IUiAction>();

            var container = builder.Build();
            ApplicationConfiguration.Initialize();
            Application.Run(container.Resolve<Form>());
        }
    }
}