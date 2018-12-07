using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagsCloudBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wordsWithFrequency = new Dictionary<string, int>();
            var withDebug = true;
            var startPoint = new Point(1000, 1000);
            var canvasSize = new Size(2000, 2000);

            foreach (var word in TxtReader.ReadAllLines("text.txt"))
            {
                var lowerWord = word.ToLower();
                if (!wordsWithFrequency.ContainsKey(lowerWord))
                    wordsWithFrequency.Add(lowerWord, 1);
                else
                    wordsWithFrequency[lowerWord] += 1;
            }

            wordsWithFrequency = wordsWithFrequency
                .RemoveWords(new List<string> { "Консультация", "Кадров", "Оценить" })
                .RemoveWordsOutOfLengthRange(leftBound: 4);

            var cloudLayouter = new CircularCloudLayouter(startPoint);
            var containerFormatter = new ContainerFormatter(wordsWithFrequency, cloudLayouter);

            Drawer.DrawAndSaveWords(canvasSize, containerFormatter.ContainersInfo, "sample.png", ImageFormat.Png, withDebug);
        }
    }
}
