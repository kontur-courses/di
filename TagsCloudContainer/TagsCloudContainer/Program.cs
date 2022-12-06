using Autofac;
using TagsCloudContainer.Actions;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainForm>().As<Form>();
            builder.RegisterType<ChoseSourceFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<AlgorithmSettingsAction>().As<IUiAction>();
            builder.RegisterType<DrawImageAction>().As<IUiAction>();

            var container = builder.Build();
            ApplicationConfiguration.Initialize();
            Application.Run(container.Resolve<Form>());
        }
    }
}