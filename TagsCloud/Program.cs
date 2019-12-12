using System;
using System.Windows.Forms;
using Autofac;
using TagsCloud.Interfaces;
using TagsCloud.MenuActions;

namespace TagsCloud
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            const string sourceTextFilePath = @"../../Resources/text.txt";
            var builder = new ContainerBuilder();
            
            builder.RegisterType<MainForm>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxImageHolder>()
                .As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FontSettings>().AsSelf().SingleInstance();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<SpiralParameters>().AsSelf().SingleInstance();
            
            builder.RegisterType<SaveImageAction>().As<IMenuAction>().SingleInstance();
            builder.RegisterType<CircularLayouterAction>().As<IMenuAction>().SingleInstance();
            builder.RegisterType<ImageSettingsAction>().As<IMenuAction>().SingleInstance();
            builder.RegisterType<FontSettingsAction>().As<IMenuAction>().SingleInstance();
            builder.RegisterType<PaletteSettingsAction>().As<IMenuAction>().SingleInstance();
            
            builder.RegisterType<LayoutPainter>().As<ILayoutPainter>().SingleInstance();
            builder.RegisterType<Random>().AsSelf().SingleInstance();
            builder.RegisterType<LayoutConstructor>().As<ILayoutConstructor>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<ArchimedeSpiral>().As<ISpiral>().SingleInstance();
            builder.RegisterType<TagsProcessor>().As<ITagsProcessor>().SingleInstance();
            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>().SingleInstance();
            builder.Register(c =>
            {
                var exHandler = c.Resolve<IExceptionHandler>();
                return new TxtReader(sourceTextFilePath, exHandler);
            }).As<ITextReader>().SingleInstance();
            builder.RegisterType<WordLengthFilter>().As<IWordFilter>().SingleInstance();
            builder.RegisterType<BoringWordsFilter>().As<IWordFilter>().SingleInstance();
            builder.RegisterType<GUIExceptionsHandler>().As<IExceptionHandler>().SingleInstance();
            var container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }
    }
}