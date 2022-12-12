using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.DefinerFontSize;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.InfrastructureUI;
using TagsCloudVisualization.InfrastructureUI.Actions;
using TagsCloudVisualization.InfrastructureUI.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public static class Program
    {
        private static IContainer ContainerBuild()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CloudForm>();
            containerBuilder.RegisterType<CloudPainter>().AsSelf().SingleInstance();

            containerBuilder.RegisterType<SetTextAction>().AsSelf().SingleInstance();

            containerBuilder.RegisterType<SaveImageAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<ButterflyCloudAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<CircleCloudAction>().As<IUiAction>().SingleInstance();

            containerBuilder.RegisterType<ImageSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<PaletteSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<ParserSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<AnalyzerSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<FontSettingsAction>().As<IUiAction>().SingleInstance();

            containerBuilder.RegisterType<DocxParser>().As<IParser>().SingleInstance();
            containerBuilder.RegisterType<TxtParser>().As<IParser>().SingleInstance();
            containerBuilder.RegisterType<DocParser>().As<IParser>().SingleInstance();

            containerBuilder.RegisterType<PaletteSettings>().As<IPaletteSettings>().SingleInstance();
            containerBuilder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<ParserSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<AnalyzerSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<FontSettings>().AsSelf().SingleInstance();

            containerBuilder.RegisterType<WordsProvider>().As<IWordsProvider>().SingleInstance();
            containerBuilder.RegisterType<DefinerFontSize.DefinerFontSize>().As<IDefinerFontSize>().SingleInstance();

            containerBuilder.RegisterType<PictureBoxImageHolder>()
                .As<PictureBoxImageHolder, IImageHolder>()
                .SingleInstance();

            containerBuilder.RegisterType<Analyzer>().As<IAnalyzer>().SingleInstance();

            return containerBuilder.Build();
        }

        [STAThread]
        public static void Main()
        {
            var container = ContainerBuild();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<CloudForm>());
        }
    }
}