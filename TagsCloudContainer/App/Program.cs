using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.App.Actions;
using TagsCloudContainer.App.Layouter;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<TagsLayouterSettingsAction>().As<IUiAction>();
            builder.RegisterType<TagsLayouterAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            builder.RegisterType<FontSettingsAction>().As<IUiAction>();

            builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.RegisterType<TagsLayouter>().AsSelf();
            builder.RegisterType<CloudLayouterSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TagsExtractor>().As<ITagsExtractor>().SingleInstance();
            builder.RegisterType<TagsPainter>().As<ITagsPainter>();
            builder.RegisterType<TwoColorsTagsPainter>().As<ITagsPainter>();
            builder.RegisterType<TwoColorsSizeTagsPainter>().As<ITagsPainter>();
            builder.RegisterType<TextReaderFromTxt>().As<ITextReader>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<FontText>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>()
                .SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ImageDirectoryProvider>().As<IImageDirectoryProvider>();

            builder.RegisterType<MainForm>().AsSelf();
            var container = builder.Build();
            var mainForm = container.Resolve<MainForm>();
            Application.Run(mainForm);
        }
    }
}
