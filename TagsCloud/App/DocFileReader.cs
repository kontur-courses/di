using System;
using System.Collections.Generic;
using MicrosoftWord = Microsoft.Office.Interop.Word;

namespace TagsCloud.App
{
    public class DocFileReader : IFileReader
    {
        public HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"doc", "docx"};

        public string[] ReadLines(string fileName)
        {
            var fileType = fileName.Split('.')[^1];
            if (!AvailableFileTypes.Contains(fileType))
                throw new ArgumentException($"Incorrect type {fileType}");
            var app = new MicrosoftWord.Application();
            var objFileName = (object) fileName;
            app.Documents.Open(ref objFileName);
            var doc = app.ActiveDocument;
            if (doc.Paragraphs.Count == 1 && doc.Paragraphs.First.Range.Text == "\r")
                return new string[0];
            var words = new string[doc.Paragraphs.Count];
            for (var i = 1; i <= words.Length; i++)
                doc.Paragraphs[i].Range.Text.Trim('\r');
            app.Quit();
            return words;
        }
    }
}