using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class WordDataHandler : IWordDataHandler
    {
        public List<CloudWordData> GetDatas(ICloudLayouter cloud, string filePath)
        {
            var words = GetWords(filePath);
            var wordWeightTuples = GetWordWeightTuples(words);
            var startPoints = GetStartPoints(cloud, wordWeightTuples);
            var data = startPoints.Select((t, i) => new CloudWordData
            { StartPoint = t, Weight = wordWeightTuples[i].Item2, Word = wordWeightTuples[i].Item1 }).ToList();
            return data;
        }

        private static List<Tuple<string, int>> GetWordWeightTuples(List<string> words) => words
            .Select(str => new Tuple<string, int>(str, words.Count(s => s == str))).Distinct()
            .OrderByDescending(t => t.Item2).ToList();

        public static Point[] GetStartPoints(ICloudLayouter cloud, List<Tuple<string, int>> wordWeightTuples)
        {
            var e = new PaintEventArgs(Graphics.FromImage(new Bitmap(100, 100)), new Rectangle());
            foreach (var wordWeightTuple in wordWeightTuples)
            {
                var stringSize = e.Graphics
                    .MeasureString(wordWeightTuple.Item1, new Font("Arial", wordWeightTuple.Item2 * 14)).ToSize();
                cloud.PutNextRectangle(stringSize);
            }

            return cloud.GetStartPointWords();
        }

        private static List<string> GetWords(string path)
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
            return words;
        }
    }
}
