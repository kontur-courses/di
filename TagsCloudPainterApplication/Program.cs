using Autofac;
using TagsCloudPainter.CloudLayouter;
using TagsCloudPainter.Drawer;
using TagsCloudPainter.FileReader;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Parser;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;
using TagsCloudPainterApplication.Actions;
using TagsCloudPainterApplication.Infrastructure;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var builder = new ContainerBuilder();

        builder.RegisterType<Palette>().AsSelf().SingleInstance();
        builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
        builder.RegisterType<TextSettings>().AsSelf().SingleInstance();
        builder.RegisterType<TagSettings>().AsSelf().SingleInstance();
        builder.RegisterType<SpiralPointerSettings>().AsSelf().SingleInstance();
        builder.RegisterType<CloudSettings>().AsSelf().SingleInstance();
        builder.RegisterType<FilesSourceSettings>().AsSelf().SingleInstance();
        builder.RegisterType<TagsCloudSettings>().AsSelf().SingleInstance();
        builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
        builder.RegisterType<CloudDrawer>().AsSelf().SingleInstance();
        builder.RegisterType<ArchimedeanSpiralPointer>().As<IFormPointer>();
        builder.RegisterType<TagsCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<TagsBuilder>().As<ITagsBuilder>().SingleInstance();
        builder.RegisterType<BoringTextParser>().As<ITextParser>().SingleInstance();
        builder.RegisterType<TextFileReader>().As<IFileReader>().SingleInstance();
        builder.RegisterType<SaveImageAction>().As<IUiAction>();
        builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
        builder.RegisterType<FileSourceSettingsAction>().As<IUiAction>();
        builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
        builder.RegisterType<DrawTagCloudAction>().As<IUiAction>();
        builder.RegisterType<MainForm>().AsSelf();

        var container = builder.Build();
        Application.Run(container.Resolve<MainForm>());
    }
}