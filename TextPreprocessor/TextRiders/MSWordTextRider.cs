using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TextPreprocessor.Core;
using Microsoft.Office.Interop.Word;

namespace TextPreprocessor.TextRiders
{
    public class MSWordTextRider : IFileTextRider
    {
        private TextRiderConfig textRiderConfig;
        
        public MSWordTextRider(TextRiderConfig textRiderConfig)
        {
            this.textRiderConfig = textRiderConfig;
        }

        public TextRiderConfig RiderConfig => textRiderConfig;

        public IEnumerable<Tag> GetTags()
        {
            return GetFileContent(textRiderConfig.FilePath)
                .Split(textRiderConfig.WordsDelimiters)
                .Select(str => textRiderConfig.GetCorrectWordFormat(str))
                .Where(str => !textRiderConfig.IsSkipWord(str))
                .Where(str => str != "")
                .Select(str => new Tag(str));
        }
        
        private string GetFileContent(string filePath)
        {
            object refFullFilePath = filePath;
            var none = Type.Missing;
            var app = new Application();
            app.Documents.Open(ref refFullFilePath,
                ref none, ref none, ref none, ref none,
                ref none, ref none, ref none, ref none,
                ref none, ref none, ref none, ref none, ref none,
                ref none, ref none);

            var document = app.Documents.Application.ActiveDocument;
            object startIndex = 0;
            object endIndex = document.Characters.Count;
            var docRange = document.Range(ref startIndex, ref endIndex);
                
            var text = docRange.Text;
            app.Quit(ref none, ref none, ref none);
            return text;
        }

        public bool CanReadFile()
        {
            return Path.GetExtension(textRiderConfig.FilePath) == ".doc"
                   || Path.GetExtension(textRiderConfig.FilePath) == ".docx";
        }
    }
}