using System;
using System.Drawing;
using System.Drawing.Imaging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloud.CloudStructure;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.WordPrework;
using CommandLine;

namespace TagsCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var options =  Parser.Default.ParseArguments<Options>(args);
            var container = new WindsorContainer();
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<ICloudLayouter>());
            container.Register(Component.For<IPointGenerator>().ImplementedBy<SpiralPointGenerator>());




            var fileReader = new FileReader(@"C:\Users\Дима\Desktop\TestFile.txt");
            var frequency = new WordAnalyzer(fileReader).GetWordFrequency();
            var spiralPointGenerator = new SpiralPointGenerator(Math.PI / 16);
            var cloud = new PointCloudLayouter(new Point(), spiralPointGenerator);

            var tagcloudLayouter = new TagCloudLayouter(new FontFamily("Arial"), cloud);

            var visualizer = new TagsCloudVisualizer(new Size(1000, 1000), "Arial");
            var bitmap1 = visualizer.GetCloudVisualization(tagcloudLayouter.GetTags(frequency));
            bitmap1.Save("cloud1.jpg", ImageFormat.Jpeg);
        }
    }
}
