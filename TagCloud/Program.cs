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
            var pictureSize = new Size(1000, 1000);
            var w = new CircularCloudLayouter(new SpiralPoints(new Point(pictureSize.Height / 2, pictureSize.Width / 2)));
            var wodsForCloud = GenerateWordsForCloud("1.txt", "Arial", 50, Color.Black);

            var r = new List<Rectangle>();
            foreach (var word in wodsForCloud)
            {
                r.Add(w.PutNextRectangle(word.WordSize));
            }

            DrawAndSaveCloud(r.ToArray(), wodsForCloud.ToArray(), "qwe.bmp", pictureSize);
        }

        public static List<WordForCloud> GenerateWordsForCloud(string path, string font, int maxWordSize, Color color)
        {
            var words = new List<string>();
            using (var fileStream = new StreamReader(path))
            {
                var text = fileStream.ReadToEnd();
                words = GetWordsFromText(text).ToList();
            }

            var wordFrequency = GetWordsFrequency(words);
            var maxFrequency = wordFrequency.Values.Max();

            return wordFrequency.Select(word => GetWordForCloud(font, maxWordSize, color, word, maxFrequency)).OrderBy(x => x.FontSize).Reverse().ToList();
        }

        private static WordForCloud GetWordForCloud(string font, int maxWordSize, Color color, KeyValuePair<string, int> word, int maxFrequency)
        {
            var wordFontSize = (int) (maxWordSize * ((double) word.Value / maxFrequency) + 0.6);
            var wordSize = new Size((int) (word.Key.Length * (wordFontSize +6) * 0.65), wordFontSize + 10);
            return new WordForCloud(font, wordFontSize, word.Key, wordSize, color);
        }

        private static Dictionary<string, int> GetWordsFrequency(List<string> words)
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word))
                    wordFrequency[word] += 1;
                else
                    wordFrequency[word] = 1;
            }

            return wordFrequency;
        }

        private static IEnumerable<string> GetWordsFromText(string text) =>
            text.Split('\n').Select(NormalizeWord);

        private static string NormalizeWord(string word)
        {
            return word;
        }

        public static void DrawAndSaveCloud(Rectangle[] rectangles, WordForCloud[] wodsForCloud, string fileName, Size pictureSize)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            var gr = Graphics.FromImage(bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, pictureSize.Width, pictureSize.Height);
            for (var i = 0; i < rectangles.Length; i++)
            {
                gr.DrawString($"{wodsForCloud[i].Word}", wodsForCloud[i].Font, Brushes.Black, rectangles[i]);
            }

            bitmap.Save(fileName);
        }
    }
}