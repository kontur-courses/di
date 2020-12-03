using System;
using TagCloud.TextConverters.TextReaders;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.PointGetters;
using TagCloud.Visualization;

namespace TagCloud.Clients
{
    internal class ConsoleClient : IClient
    {
        private readonly ITextReader reader = new TextReaderTxt();
        private readonly ITextProcessor processor;
        private readonly IWordsMetric metric;
        private readonly IPointGetter pointGetter;

        internal ConsoleClient(ITextProcessor processor, 
            IWordsMetric metric, IPointGetter pointGetter)
        {
            this.processor = processor;
            this.metric = metric;
            this.pointGetter = pointGetter;
        }

        public void Run()
        {
            string answear = null;
            Console.WriteLine("Hello, I'm your personal visualization client");
            while (answear != "exit")
            {
                Console.WriteLine("Please, write path to file with words");
                answear = Console.ReadLine();
                if (answear == "exit")
                    break;
                var text = reader.ReadText(answear);
                if (text == null)
                {
                    Console.WriteLine("Something was wrong, Please try again");
                    continue;
                }
                var info = ReadInfo();
                Console.WriteLine("Please write path to save picture");
                var path = Console.ReadLine();
                Visualization(text, path, info);
                Console.WriteLine("Picture save");
                Console.WriteLine();
                break;
            }
        }

        private VisualizationInfo ReadInfo()
        {
            Console.WriteLine("Write 3 numbers from 0 to 255 between space");
            Console.WriteLine("For example: 255 176 0");
            var colorRGB = Console.ReadLine();
            var color = VisualizationInfo.ReadColor(colorRGB);
            Console.WriteLine("Please write font");
            var font = Console.ReadLine();
            Console.WriteLine("Please write 2 number for size picture");
            var sizeString = Console.ReadLine();
            var size = VisualizationInfo.ReadSize(sizeString);
            return new VisualizationInfo(size, font, color);
        }

        public void Visualization(string text, string picturePath, VisualizationInfo info)
        {
            var tagCloud = AlgoritmTagCloud.GetTagCloud(text, pointGetter, processor, metric);
            TagCloudVisualization.Visualization(tagCloud, picturePath, info);
        }
    }
}
