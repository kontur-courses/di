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
        private const int LetterWidth = 30;
        private const int LetterHeight = 40;
        private const string PathToText = "../../../Texts/SourceText2.txt";
        private const string PathToBoringWords = "../../../Texts/BoringWords.txt";
        private const string PathToDictionary = "../../../Texts/ru_RU.dic";
        private const string PathToAffix = "../../../Texts/ru_RU.aff";
        private const string FontName = "Times New Roman";
        private const double SpiralParameter = 0.005;

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

        public List<Rectangle> GetRectangles(Size imageSize, List<(string, int)> words)
        {
            var cloud = new CircularCloudLayouter(new Point(imageSize.Width / 2, imageSize.Height / 2), SpiralParameter);

            return words.Select(word => cloud.PutNextRectangle(
                new Size(
                    (int)(LetterWidth * Math.Log(word.Item2 + 1) * word.Item1.Length),
                    (int)(LetterHeight * Math.Log(word.Item2 + 1))))).ToList();
        }
    }
}
