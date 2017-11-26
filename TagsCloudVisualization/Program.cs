using System.Drawing;
using System.IO;
using Autofac;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudCenter = new Point(400, 400);
            var container = new ContainerBuilder();
            container.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>()
                .WithParameter("cloudCenter", cloudCenter);
            
            
            
            
            var input = File.ReadLines("book1.txt");
            var frequencyDict = new FileParser(150, 4).GetWordsFrequensy(input);
            var cloudLAtouter = new CircularCloudLayouter(cloudCenter);
            CloudTagDrawer.DrawTagsToForm(cloudCenter, new TagHandler()
                .MakeTagRectangles(frequencyDict, cloudCenter, cloudLAtouter) ,800, 800);

//            CloudTagMaker.GetCloudImage(input);
        }
    }

}
