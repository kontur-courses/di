using TagCloud.Clients;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.PointGetters;
using TagCloud.CloudLayoters;
using System.Drawing;

namespace TagCloud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new ParagraphTextProcessor();
            var metric = new CountWordMetric();
            var getter = new CirclePointGetter(Point.Empty);
            IClient client = new ConsoleClient(processor, metric, new DensityCloudLayouter(getter));
            client.Run();
        }
    }
}
