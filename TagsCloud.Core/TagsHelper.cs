using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloud.Core
{
    public static class TagsHelper
    {
        public static List<(string, int)> GetWords(string pathToText, string pathToBoringWords,
            string pathToDictionary, string pathToAffix)
        {
            string mainText;
            Hunspell hunspell;
            HashSet<string> boringWords;

            try
            {
                mainText = File.ReadAllText(pathToText);
                hunspell = new Hunspell(pathToAffix, pathToDictionary);
                boringWords = TextAnalyzer.SplitTextOnUniqueWords(File.ReadAllText(pathToBoringWords));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

            return TextAnalyzer.GetWordByFrequency(
                mainText,
                boringWords,
                hunspell,
                x => x.OrderByDescending(y => y.Value)
                    .ThenByDescending(y => y.Key.Length)
                    .ThenBy(y => y.Key, StringComparer.Ordinal));
        }

        public static List<Rectangle> GetRectangles(Size imageSize, List<(string, int)> words, double spiralParameter,
            double letterWidth)
        {
            var cloud = new CircularCloudLayouter(new Point(imageSize.Width / 2, imageSize.Height / 2),
                spiralParameter);

            return words.Select(word => cloud.PutNextRectangle(
                new Size(
                    (int) (letterWidth / 1.4 * Math.Log(word.Item2 + 1) * word.Item1.Length),
                    (int) (letterWidth * Math.Log(word.Item2 + 1))))).ToList();
        }
    }
}