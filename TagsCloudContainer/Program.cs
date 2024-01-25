using Autofac;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Client;
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

            builder.RegisterType<MainForm>().As<Form>();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FileSettings>().AsSelf().SingleInstance();
            builder.RegisterType<AlgorithmSettings>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();
            builder.RegisterType<FileParser>().As<IFileParser>();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TagCloudDrawer>().As<IDrawer>();
            builder.RegisterType<SelectBoringWordsFileAction>().As<IUiAction>();
            builder.RegisterType<SelectSourceFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<AlgorithmSettingsAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<DrawTagCloudAction>().As<IUiAction>();
            builder.RegisterType<GUITagCloudClient>().As<ITagCloudClient>();
            builder.RegisterType<RectanglePlacer>().As<IRectanglePlacer>();

            return builder;
        }
    }
}