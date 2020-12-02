using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using NHunspell;

namespace TagsCloud.Core
{
    public static class TagsHelper
    {
        public static List<(string, int)> GetWords(string pathToFile, string pathToBoringWords,
            string pathToDictionary, string pathToAffix)
        {
            string mainText;
            Hunspell hunspell;
            HashSet<string> boringWords;

            try
            {
                mainText = GetTextFromFile(pathToFile);
                hunspell = new Hunspell(pathToAffix, pathToDictionary);
                boringWords = TextAnalyzer.SplitTextOnUniqueWords(GetTextFromFile(pathToBoringWords));
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
            var spiral = new ArchimedeanSpiral(new Point(imageSize.Width / 2, imageSize.Height / 2), spiralParameter);
            var cloud = new CircularCloudLayouter(spiral);

            return words.Select(word => cloud.PutNextRectangle(
                new Size(
                    (int) (letterWidth / 1.4 * Math.Log(word.Item2 + 1) * word.Item1.Length),
                    (int) (letterWidth * Math.Log(word.Item2 + 1))))).ToList();
        }

        public static string GetTextFromFile(string document)
        {
            switch (document.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last())
            {
                case "html":
                case "aff":
                case "dic":
                case "doc":
                case "txt":
                    return File.ReadAllText(document);
                case "docx":
                    var wordDocument = WordprocessingDocument.Open(document, false);
                    return wordDocument.MainDocumentPart.Document.Body.InnerText;
                default:
                    throw new ArgumentException("not valid path to file");
            }
        }
    }
}