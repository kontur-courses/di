using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudForm.Actions;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;
using TagsCloudForm.WordFilters;
using CircularCloudLayouter;

namespace TagsCloudForm
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CloudForm>().As<CloudForm>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<global::CircularCloudLayouter.CircularCloudLayouter>().As<ICircularCloudLayouter>();
            RegisterPainters(builder);
            RegisterWordFiltersAndFunctions(builder);
            RegisterActions(builder);
            RegisterSettingsClasses(builder);
            builder.RegisterType<XmlObjectSerializer>().As<IObjectSerializer>();
            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();

            var container = builder.Build();
            var form = container.Resolve<CloudForm>();
            Application.Run(form);
        }

        private static void RegisterWordFiltersAndFunctions(ContainerBuilder builder)
        {
            builder.RegisterType<SpellCheckerFilter>().As<IWordsFilter>();
            builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();
            builder.RegisterType<PartOfSpeechFilter>().As<IWordsFilter>();
            builder.RegisterType<WordsFrequencyParser>().As<IWordsFrequencyParser>().SingleInstance();
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterWithWordsAction>().As<IUiAction>();
            builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
        }

        private static void RegisterSettingsClasses(ContainerBuilder builder)
        {
            builder.RegisterType<CircularCloudLayouterSettings>().As<CircularCloudLayouterSettings>();
            builder.Register(x => x.Resolve<AppSettings>().ImageSettings).As<ImageSettings>().SingleInstance();
            builder.RegisterType<SettingsManager>().As<SettingsManager>();
            builder.Register(x => x.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>().SingleInstance();
        }

        private static void RegisterPainters(ContainerBuilder builder)
        {
            builder.RegisterType<CloudPainter>();
            builder.RegisterType<CloudWithWordsPainter>();
        }
    }
}
