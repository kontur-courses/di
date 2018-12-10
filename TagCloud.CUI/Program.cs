using System.Drawing;
using Autofac;
using Autofac.Core;
using TagCloud.Core.Layouters;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking;
using TagCloud.Core.TextWorking.WordsProcessing;
using TagCloud.Core.TextWorking.WordsProcessing.ProcessingUtilities;
using TagCloud.Core.TextWorking.WordsReading;
using TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles;
using TagCloud.Core.Visualizers;

namespace TagCloud.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Core.TagCloud>().AsSelf();

            builder.RegisterType<TextWorker>().AsSelf();
            builder.RegisterType<TxtWordsReader>().As<IWordsReaderForFile>();
            builder.RegisterType<GeneralWordsReader>().As<IWordsReader>();
            builder.RegisterType<LowerCaseUtility>().As<IProcessingUtility>();
            builder.RegisterType<SimpleWordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<TextWorkingSettings>().AsSelf();

            builder.RegisterType<SimpleTagCloudVisualizer>().As<ITagCloudVisualizer>();
            builder.RegisterType<VisualizingSettings>().AsSelf();
            builder.Register(ctx => new CircularCloudLayouter(new PointF(400, 300))).As<ICloudLayouter>();
            builder.RegisterType<OneColorPainter>().As<IPainter>();
            builder.RegisterType<PaintingSettings>().AsSelf();

            var container = builder.Build();
            
            var tagCloud = container.Resolve<Core.TagCloud>();
        }
    }
}
