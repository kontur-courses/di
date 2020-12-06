using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using NHunspell;
using RTFToTextConverter;
using TagsCloud.Common;

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

        public static string GetTextFromFile(string document)
        {
            switch (document.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last())
            {
                case "aff":
                case "dic":
                case "txt":
                    return File.ReadAllText(document);
                case "rtf":
                    return RTFToText.converting().rtfFromFile(document);
                case "docx":
                    var wordDocument = WordprocessingDocument.Open(document, false);
                    return wordDocument.MainDocumentPart.Document.Body.InnerText;
                default:
                    throw new ArgumentException("not valid path to file");
            }
        }

        public static List<Rectangle> GetRectangles(ICircularCloudLayouter cloud, 
            List<(string, int)> words, Dictionary<int, Font> newFonts, Font font)
        {
            var rectangles = new List<Rectangle>();
            foreach (var word in words)
            {
                var fontSize = word.Item2;
                if (!newFonts.ContainsKey(fontSize))
                    newFonts[fontSize] = new Font(font.FontFamily, (int)(font.Size * Math.Log(word.Item2 + 1)), font.Style);

                var rect = cloud.PutNextRectangle(new Size((int)newFonts[fontSize].Size * word.Item1.Length,
                    newFonts[fontSize].Height));
                rectangles.Add(rect);
            }
            return rectangles;
        }
    }
}