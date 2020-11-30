using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NHunspell;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public class TagsCreator
    {
        private const string PathToText = "../../../Texts/SourceText2.txt";
        private const string PathToBoringWords = "../../../Texts/BoringWords.txt";
        private const string PathToDictionary = "../../../Texts/ru_RU.dic";
        private const string PathToAffix = "../../../Texts/ru_RU.aff";

        public List<(string, int)> GetWords()
        {
            var hunspell = new Hunspell(PathToAffix, PathToDictionary);
            var boringWords = TextAnalyzer.SplitTextOnUniqueWords(File.ReadAllText(PathToBoringWords));

            return TextAnalyzer.GetWordByFrequency(
                File.ReadAllText(PathToText),
                boringWords,
                hunspell,
                x => x.OrderByDescending(y => y.Value)
                    .ThenByDescending(y => y.Key.Length)
                    .ThenBy(y => y.Key, StringComparer.Ordinal));
        }

        public List<Rectangle> GetRectangles(Size imageSize, List<(string, int)> words, double spiralParameter, double letterWidth)
        {
            var cloud = new CircularCloudLayouter(new Point(imageSize.Width / 2, imageSize.Height / 2), spiralParameter);

            return words.Select(word => cloud.PutNextRectangle(
                new Size(
                    (int)(letterWidth / 1.4 * Math.Log(word.Item2 + 1) * word.Item1.Length),
                    (int)(letterWidth * Math.Log(word.Item2 + 1))))).ToList();
        }
    }
}
