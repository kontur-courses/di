﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordDataProvider : IWordDataProvider
    {
        public List<CloudWordData> GetData(ICloudLayouter cloud, string filePath)
        {
            var settings = new WordsExtractorSettings();
            var extractor = new WordsExtractor(settings);
            var words = extractor.Extract(filePath);
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

            var startPointWords = cloud.GetRectangles().Select(r => new Point(r.Left, r.Top)).ToArray();
            return startPointWords;
        }
    }
}
