using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudWordsForm
    {
        private static void Main(string[] args)
        {
            //var parameters = CloudParametersParser.Parse(args);

            //if (!parameters.IsCorrect())
            //    return;

            var sortedWords = GetSortedWords("input.txt");
            var cloud = new CircularCloudLayouter(new Spiral(0.1, Math.PI / 36));
            var startPoints = GetData(cloud, sortedWords);

            var picture = RectangleTagsCloudVisualizer.GetPicture(startPoints, sortedWords, Color.Black);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }

        public static Point[] GetData(CircularCloudLayouter cloud, List<KeyValuePair<string, int>> sortedWords)
        {
            var e = new PaintEventArgs(Graphics.FromImage(new Bitmap(100, 100)), new Rectangle());
            foreach (var word in sortedWords)
            {
                var stringSize = e.Graphics.MeasureString(word.Key, new Font("Arial", word.Value * 14)).ToSize();
                cloud.PutNextRectangle(stringSize);
            }

            return cloud.GetStartPointWords();
        }

        private static List<KeyValuePair<string, int>> GetSortedWords(string path)
        {
            var stopChars = new[]
                {"?", "@", ",", ".", ";", ")", "(", ":", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            var stopWords = new[]
            {
                "the", "and", "to", "a", "of", "in", "on", "at", "that",
                "as", "but", "with", "out", "for", "up", "one", "from", "into"
            };

            var text = File.ReadAllText(path, Encoding.Default).Replace("\n", " ").Replace("\r", " ");
            text = stopChars.Aggregate(text, (current, c) => current.Replace(c, string.Empty));
            var words = text.Split(' ').Where(w => w != string.Empty && !stopWords.Contains(w))
                .Select(w => w.ToLowerInvariant()).ToList();
            var wordsDict = new Dictionary<string, int>();
            foreach (var word in words)
                if (!wordsDict.ContainsKey(word))
                    wordsDict.Add(word, 1);
                else
                    wordsDict[word]++;

            var sortedWords = wordsDict.OrderByDescending(w => w.Value).ToList();
            return sortedWords;
        }
    }
}