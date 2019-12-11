using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.CloudLayouter.CircularLayouter;
using TagCloud.TextColoration;
using TagCloud.TextConversion;
using TagCloud.TextFilterConditions;
using TagCloud.TextFiltration;
using TagCloud.TextParser;
using TagCloud.TextProvider;
using TagCloud.Visualization;
using TagCloudForm.Holder;
using TagCloudForm.Settings;

namespace TagCloudForm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<CloudPainter>().AsSelf().SingleInstance();
            builder.RegisterType<TagCloudForm>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Action") || t.Name.EndsWith("Condition"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.LoadFrom("TagCloud.dll"))
                .Where(t => t.Name.EndsWith("Condition"))
                .AsImplementedInterfaces();

            builder.RegisterType<WordLengthCondition>().As<IFilterCondition, WordLengthCondition>().SingleInstance();
            builder.RegisterType<CloudVisualization>().AsSelf().SingleInstance();
            builder.RegisterType<AppSettings>().As<IDirectoryProvider>().SingleInstance();
            builder.RegisterType<AppSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<Size>().AsSelf();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<ViewSettings>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistMaker>().AsSelf().SingleInstance();
            builder.RegisterType<TextFilter>().AsSelf().SingleInstance();
            builder.RegisterType<TextConverter>().AsSelf().SingleInstance();
            builder.RegisterType<TextParser>().As<ITextParser>().SingleInstance();
            builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiral>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TxtFileReader>().As<ITextProvider, TxtFileReader>().SingleInstance();
            builder.RegisterType<FrequencyDictionaryMaker>().AsSelf().SingleInstance();
            builder.RegisterType<RandomTextColoration>().As<ITextColoration>();
            builder.RegisterType<ToLowerCaseConversion>().As<ITextConversion>();

            var container = builder.Build();
            Application.Run(container.Resolve<TagCloudForm>());
        }
    }
}