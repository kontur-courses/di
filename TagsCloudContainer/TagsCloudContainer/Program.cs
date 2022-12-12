using System.Reflection.Metadata;
using Autofac;
using DeepMorphy;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;
using TagsCloudContainer.Visualisator;

namespace TagsCloudContainer
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();

            RegisterDependencies(builder);

            try
            {
                var container = builder.Build();
                ApplicationConfiguration.Initialize();
                Application.Run(container.Resolve<Form>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<MainForm>().As<Form>();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FileSettings>().AsSelf().SingleInstance();
            builder.RegisterType<AlgorithmSettings>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();
            builder.RegisterType<Algorithm.Parser>().As<IParser>();
            builder.RegisterType<MorphAnalyzer>().AsSelf().SingleInstance();
            builder.RegisterType<WordsCounter>().As<IWordsCounter>();
            builder.RegisterType<Algorithm.CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TagCloudPainter>().As<IPainter>();
            builder.RegisterType<ChoseSourceFileAction>().As<IUiAction>();
            builder.RegisterType<ChoseBoringWordsSourceFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<AlgorithmSettingsAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<DrawImageAction>().As<IUiAction>();
            builder.RegisterType<GuiTagCloudService>().As<ITagCloudService>();
        }
    }
}