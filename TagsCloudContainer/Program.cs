using Autofac;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;
using TagsCloudContainer.Infrastucture.Visualization;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                var builder = CreateBuilder();
                var container = builder.Build();

                ApplicationConfiguration.Initialize();
                Application.Run(container.Resolve<Form>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static ContainerBuilder CreateBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainForm>().As<Form>().InstancePerLifetimeScope();
            builder.RegisterType<ImageSettings>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FileSettings>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AlgorithmSettings>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PictureBox>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FileParser>().As<IFileParser>().InstancePerLifetimeScope();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().InstancePerLifetimeScope();
            builder.RegisterType<RectanglePlacer>().As<IRectanglePlacer>().InstancePerLifetimeScope();
            builder.RegisterType<TagCloudDrawer>().As<IDrawer>().InstancePerLifetimeScope();
            builder.RegisterType<SelectBoringWordsFileAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<SelectSourceFileAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<SaveImageAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<AlgorithmSettingsAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<DrawTagCloudAction>().As<IUiAction>().InstancePerLifetimeScope();
            builder.RegisterType<GUITagCloudClient>().As<ITagCloudClient>().InstancePerLifetimeScope();

            return builder;
        }
    }
}