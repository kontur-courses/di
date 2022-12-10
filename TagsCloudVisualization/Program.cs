using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
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

            containerBuilder.RegisterType<SaveImageAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<SetTextAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<ButterflyCloudAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<CircleCloudAction>().As<IUiAction>().SingleInstance();

            containerBuilder.RegisterType<ImageSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<PaletteSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<ParserSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<AnalyzerSettingsAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<FontSettingsAction>().As<IUiAction>().SingleInstance();

            containerBuilder.RegisterType<ParserDocx>().As<IParser>().SingleInstance();
            containerBuilder.RegisterType<ParserTxt>().As<IParser>().SingleInstance();
            containerBuilder.RegisterType<ParserDoc>().As<IParser>().SingleInstance();

            containerBuilder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<PaletteSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<ParserSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<AnalyzerSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<FontSettings>().AsSelf().SingleInstance();

            containerBuilder.RegisterType<TextFileProvider>()
                .As<TextFileProvider, ICurrentTextFileProvider>()
                .SingleInstance();
            containerBuilder.RegisterType<PictureBoxImageHolder>()
                .As<PictureBoxImageHolder, IImageHolder>()
                .SingleInstance();

            containerBuilder.Register<Func<DefinerSize, ICurve, CloudPainter>>(c =>
            {
                var holder = c.Resolve<IImageHolder>();
                var analyzer = c.Resolve<IAnalyzer>();
                var palette = c.Resolve<PaletteSettings>();
                return (size, curve) => new CloudPainter(holder, analyzer, size, curve, palette);
            }).SingleInstance();


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