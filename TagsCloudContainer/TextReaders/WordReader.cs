using System;
using System.Text;
using Xceed.Words.NET;

namespace TagsCloudContainer.TextReaders
{
    public class WordReader : ITextReader
    {
        public string GetTextFromFile(string path)
        {
            using var document = DocX.Load(path);

            var paragraphs = document.Paragraphs;
            var builder = new StringBuilder();
            foreach (var paragraph in paragraphs)
            {
                var word = paragraph.Text.ToLower();
                builder.Append(word + Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}