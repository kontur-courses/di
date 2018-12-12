using System;
using System.Windows.Forms;
using Autofac;
using TagCloud.Core.Layouters;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking;
using TagCloud.Core.TextWorking.WordsProcessing;
using TagCloud.Core.TextWorking.WordsProcessing.ProcessingUtilities;
using TagCloud.Core.TextWorking.WordsReading;
using TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles;
using TagCloud.Core.Visualizers;

namespace TagCloud.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();
            InjectDependencies(builder);
            var container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }

        static void InjectDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<MainForm>().AsSelf();

            builder.RegisterType<Core.TagCloud>().AsSelf();
            builder.RegisterSettings<TagCloudSettings>();

            builder.RegisterType<TextWorker>().AsSelf();
            builder.RegisterType<TxtWordsReader>().As<IWordsReaderForFile>();
            builder.RegisterType<TxtWordsReader>().As<IWordsReaderForFile>();
            builder.RegisterType<GeneralWordsReader>().As<IWordsReader>();
            builder.RegisterType<LowerCaseUtility>().As<IProcessingUtility>();
            builder.RegisterType<SimpleWordsProcessor>().As<IWordsProcessor>();
            builder.RegisterSettings<TextWorkingSettings>();

            builder.RegisterType<SimpleTagCloudVisualizer>().As<ITagCloudVisualizer>();
            builder.RegisterSettings<VisualizingSettings>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterSettings<LayoutingSettings>();
            builder.RegisterType<OneColorPainter>().As<IPainter>();
            builder.RegisterSettings<PaintingSettings>();
        }
    }
}
