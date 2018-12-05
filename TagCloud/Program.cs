using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagCloud.Layouters;
using TagCloud.Painters;
using TagCloud.TextReaders;
using TagCloud.Visualizers;
using TagCloud.WordsProcessors;
using TagCloud.WordsProcessors.ProcessingUtilities;
using TagCloud.WordsReaders;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LowerCaseUtility>().As<IProcessingUtility>();
            
            var layouter = new CircularCloudLayouter(new PointF(500, 500));
            builder.RegisterInstance(layouter).As<AbstractCloudLayouter>();
            
            var imageSettings = new ImageSettings(1000, 1000, ImageFormat.Png, new Font("arial", 30), 10, 50);
            builder.RegisterInstance(imageSettings).As<ImageSettings>();

            var painter = new OneColorPainter(Color.White, Brushes.DarkSlateBlue);
            builder.RegisterInstance(painter).As<IPainter>();

            builder.RegisterType<TxtWordsReader>().As<IWordsReader>();
            builder.RegisterType<SimpleWordsProcessor>().As<AbstractWordsProcessor>();
            builder.RegisterType<SimpleTagCloudVisualizer>().As<AbstractTagCloudVisualizer>();

            builder.RegisterType<TagCloud>();

            var container = builder.Build();

            var tagCloud = container.Resolve<TagCloud>();
            tagCloud.MakeTagCloud("text.txt", "res.png");
        }
    }
}
