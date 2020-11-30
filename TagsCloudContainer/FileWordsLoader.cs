using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudContainer
{
    public class FileWordsLoader : IWordsLoader
    {
        private readonly string pathToFile;
        private readonly string[] supportedExt = {".docx", ".txt"};

        public FileWordsLoader(string pathToFile)
        {
            if (!File.Exists(pathToFile))
                throw new ArgumentException($"The specified file does not exist: {pathToFile}");
            
            if (!supportedExt.Contains(Path.GetExtension(pathToFile)))
                throw new ArgumentException($"Format of this file isn't supported: {pathToFile}");

            this.pathToFile = pathToFile;
        }
        
        public string[] GetWords()
        {
            return Path.GetExtension(pathToFile) switch
            {
                ".docx" => FromDocx(),
                ".txt" => FromTxt()
            };
        }

        private string[] FromDocx()
        {
            var words = new List<string>();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(pathToFile, false))
            {
                var paragraphs = wordDocument.MainDocumentPart.Document.Body.OfType<Paragraph>();
                words.AddRange(paragraphs.Select(paragraph => paragraph.InnerText));
            }
            
            return words.ToArray();
        }

        private string[] FromTxt()
        {
            return File.ReadAllLines(pathToFile);
        }
    }
}