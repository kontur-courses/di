using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputFile = "1.txt";
            var outputFile = "qwe.bmp";

            var wordNormalizer = new WordsNormalizer();
            var wordForCloudGenerator = new WordsForCloudGenerator("Arial", Color.Black, 60);
            var pictureSize = new Size(1000, 1000);
            var circularCloudLayouter =
                new CircularCloudLayouter(new SpiralPoints(new Point(pictureSize.Height / 2, pictureSize.Width / 2)));


            var words = GetWordsFromFile(inputFile);
            var normalizeWords = wordNormalizer.NormalizeWords(words);
            var wordsForCloud = wordForCloudGenerator.Generate(normalizeWords);


            DrawAndSaveCloud(
                wordsForCloud.Select(word => circularCloudLayouter.PutNextRectangle(word.WordSize)).ToList(),
                wordsForCloud, outputFile, pictureSize);
        }

        private static List<string> GetWordsFromFile(string path)
        {
            using (var fileStream = new StreamReader(path))
            {
                return fileStream.ReadToEnd().Split('\n').ToList();
            }
        }

        private static void DrawAndSaveCloud(List<Rectangle> rectangles, List<WordForCloud> wordsForCloud,
            string fileName,
            Size pictureSize)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            var gr = Graphics.FromImage(bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, pictureSize.Width, pictureSize.Height);
            for (var i = 0; i < rectangles.Count; i++)
            {
                gr.DrawString($"{wordsForCloud[i].Word}", wordsForCloud[i].Font,
                    new SolidBrush(wordsForCloud[i].WordColor), rectangles[i]);
            }

            bitmap.Save(fileName);
        }
    }
}