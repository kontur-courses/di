using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;

namespace TagCloud.WordsProvider
{
    public class MicrosoftWordWordsProvider : FileWordsProvider
    {
        public MicrosoftWordWordsProvider(string filePath) : base(filePath)
        {
        }

        protected override bool CheckFile(string filePath)
        {
            return filePath.EndsWith(".doc") || filePath.EndsWith(".docx");
        }

        public override IEnumerable<string> GetWords()
        {
            var application = new Application();
            var document = application.Documents.Open(FilePath);
            var words = (from Range word in document.Words select word.Text).ToList();
            application.Quit();
            return words;
        }
    }
}