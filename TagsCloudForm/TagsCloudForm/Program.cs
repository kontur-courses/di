using System;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudForm.Actions;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;
using TagsCloudForm.WordFilters;
using CircularCloudLayouter;
using TagsCloudForm.CircularCloudLayouterSettings;
using TagsCloudForm.CloudPainters;

namespace TagsCloudForm
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CloudForm>().As<CloudForm>();
            builder.RegisterType<Palette>().As<IPalette>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter.CircularCloudLayouter>().As<ICircularCloudLayouter>();
            RegisterPainters(builder);
            RegisterWordFiltersAndFunctions(builder);
            RegisterActions(builder);
            RegisterSettingsClasses(builder);
            RegisterFactories(builder);
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
            builder.RegisterType<CircularCloudLayouterAction>().As<IUiAction>().WithParameter(ResolvedParameter.ForNamed<IPainterFactory>("CircularCloudLayouterAction"));
            builder.RegisterType<CircularCloudLayouterWithWordsAction>().As<IUiAction>().WithParameter(ResolvedParameter.ForNamed<IPainterFactory>("CircularCloudLayouterWithWordsAction"));
            builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterSettingsAction>().As<IUiAction>();
            builder.RegisterType<LayouterWithWordsSettingsAction>().As<IUiAction>();
            builder.RegisterType<GraphicDrawer>().As<IGraphicDrawer>();
        }

        private static void RegisterSettingsClasses(ContainerBuilder builder)
        {
            builder.RegisterType<CircularCloudLayouterSettings.CircularCloudLayouterSettings>().As<CircularCloudLayouterSettings.CircularCloudLayouterSettings, ICircularCloudLayouterSettings>().SingleInstance();
            builder.RegisterType<CircularCloudLayouterWithWordsSettings>().As<CircularCloudLayouterWithWordsSettings, ICircularCloudLayouterWithWordsSettings>().SingleInstance();
            builder.Register(x => x.Resolve<AppSettings>().ImageSettings).As<ImageSettings>().SingleInstance();
            builder.RegisterType<SettingsManager>().As<SettingsManager>();
            builder.Register(x => x.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>().SingleInstance();
        }

        private static void RegisterPainters(ContainerBuilder builder)
        {
            builder.RegisterType<CloudPainter>();
            builder.RegisterType<CloudWithWordsPainter>();
        }

        private static void RegisterFactories(ContainerBuilder builder)
        {
            builder.RegisterType<CloudPainterFactory>().Named<IPainterFactory>("CircularCloudLayouterAction");
            builder.RegisterType<CloudWithWordsPainterFactory>().Named<IPainterFactory>("CircularCloudLayouterWithWordsAction");
        }
    }
}
