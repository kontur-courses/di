using TagCloud.Clients;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.PointGetters;
using System.Drawing;

namespace TagCloud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new TextProcessor();
            var metric = new CountWordMetric();
            var getter = new CirclePointGetter(Point.Empty);
            IClient client = new ConsoleClient(processor, metric, getter);
            client.Run();
        }
    }
}
