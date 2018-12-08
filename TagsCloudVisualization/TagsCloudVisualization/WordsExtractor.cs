using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Office.Interop.Word;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class WordsExtractor : IWordsExtractor
    {
        public List<string> Extract(string path, IWordsExtractorSettings settings)
        {
            var format = path.Substring(path.IndexOf(".", StringComparison.Ordinal) + 1);
            var text = GetTotalText(path, format);

            text = text
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Replace("\t", " ");
            text = settings.StopChars.Aggregate(text, (current, c) => current.Replace(c, ' '));
            var words = text.Split(' ')
                .Where(w => w.Length >= 3 && w != string.Empty && !settings.StopWords.Contains(w))
                .Select(w => w.Trim().ToLowerInvariant()).ToList();
            return words;
        }

        private static string GetTotalText(string path, string format)
        {
            if (format.StartsWith("doc"))
            {
                var textBuilder = new StringBuilder();
                var word = new Application();
                object miss = Missing.Value;
                object fileName = Path.IsPathRooted(path)
                    ? path
                    : $"{System.Windows.Forms.Application.StartupPath}\\{path}";
                var docs = word.Documents.Open(ref fileName, ref miss, ref miss, ref miss, ref miss, ref miss,
                    ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

                for (var i = 0; i < docs.Paragraphs.Count; i++)
                    textBuilder.Append(docs.Paragraphs[i + 1].Range.Text);

                docs.Close();
                word.Quit();
                return textBuilder.ToString();
            }

            if (format.Equals("txt"))
                return File.ReadAllText(path, Encoding.Default);

            throw new ArgumentException($"Unknown format: {format}");
        }
    }
}