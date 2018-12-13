using Autofac;
using TagsCloudVisualization.App;
using TagsCloudVisualization.App.Actions;
using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.TagsCloud;
using TagsCloudVisualization.TagsCloud.CircularCloud;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization
{
    public class DependencyBuilder
    {
        public ContainerBuilder CreateContainer()
        {
            var container = new ContainerBuilder();
            RegisterUiActions(container);
            RegisterSettings(container);
            container.RegisterType<Palette>().AsSelf().SingleInstance();
            container.RegisterType<TagsCloudVisualizer>().AsSelf().SingleInstance();
            container.RegisterType<PictureBoxImageHolder>().AsSelf().SingleInstance();
            container.RegisterType<MainForm>().AsSelf();
            return container;
        }

        private void RegisterSettings(ContainerBuilder container)
        {
            container.RegisterType<ImageSettings>().As<IImageSettings>().SingleInstance();
            container.RegisterType<WordsSettings>().As<IWordsSettings>().SingleInstance();
            container.RegisterType<TagsCloudSettings>().As<ITagsCloudSettings>().SingleInstance();
        }

        private void RegisterUiActions(ContainerBuilder container)
        {
            container.RegisterType<OpenFileAction>().As<IUiAction>();
            container.RegisterType<CompressedTagsCloudAction>().As<IUiAction>();
            container.RegisterType<SaveImageAction>().As<IUiAction>();
            container.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            container.RegisterType<ImageSettingsAction>().As<IUiAction>();
            container.RegisterType<TagsCloudAction>().As<IUiAction>();
        }
    }
}