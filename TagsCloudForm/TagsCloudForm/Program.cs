using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudForm.Actions;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;
using TagsCloudForm.WordFilters;

namespace TagsCloudForm
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterWithWordsAction>().As<IUiAction>();
            builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            builder.RegisterType<CloudForm>().As<CloudForm>();
            builder.RegisterType<CircularCloudLayouterSettings>().As<CircularCloudLayouterSettings>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<SpellCheckerFilter>();

            builder.RegisterType<CircularCloudLayouter.CircularCloudLayouter>();

            builder.RegisterType<CloudPainter>();

            builder.RegisterType<CloudWithWordsPainter>();

            builder.RegisterType<BoringWordsFilter>();

            builder.RegisterType<PartOfSpeechFilter>();

            builder.RegisterType<XmlObjectSerializer>().As<IObjectSerializer>();

            builder.Register(x => x.Resolve<AppSettings>().ImageSettings).As<ImageSettings>().SingleInstance();

            builder.RegisterType<SettingsManager>().As<SettingsManager>();


            builder.Register(x => x.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>().SingleInstance();

            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>();

            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();

            builder.RegisterType<WordsFrequencyParser>().As<IWordsFrequencyParser>().SingleInstance();

            var container = builder.Build();

            var form = container.Resolve<CloudForm>();


            Application.Run(form);
        }
    }
}
