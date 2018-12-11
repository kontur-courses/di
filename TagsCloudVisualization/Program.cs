using System;
using System.Windows.Forms;
using TagsCloudVisualization.App;
using TagsCloudVisualization.WordProcessing;
using Autofac;
using TagsCloudVisualization.App.Actions;
using TagsCloudVisualization.TagsCloud;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace TagsCloudVisualization
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = ContainerCreation();
            var form = container.Build().Resolve<MainForm>();
            Application.Run(form);
        }

        public static ContainerBuilder ContainerCreation()
        {
            var container = new ContainerBuilder();
            container.RegisterType<OpenFileAction>().As<IUiAction>();
            container.RegisterType<CompressedTagsCloudAction>().As<IUiAction>();
            container.RegisterType<SaveImageAction>().As<IUiAction>();
            container.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            container.RegisterType<ImageSettingsAction>().As<IUiAction>();
            container.RegisterType<TagsCloudAction>().As<IUiAction>();
            container.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            container.RegisterType<Palette>().AsSelf().SingleInstance();
            container.RegisterType<WordsSettings>().AsSelf().SingleInstance();
            container.RegisterType<PictureBoxImageHolder>().AsSelf().SingleInstance();
            container.RegisterType<TagsCloudVisualizer>().AsSelf().SingleInstance();
            container.RegisterType<TagsCloudSettings>().AsSelf().SingleInstance();
            container.RegisterType<MainForm>().AsSelf();
            return container;
        }
    }
}
