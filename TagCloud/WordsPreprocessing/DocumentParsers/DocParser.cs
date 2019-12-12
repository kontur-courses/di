using System;
using System.Collections.Generic;
using TagCloud.Interfaces;
using MsWord = Microsoft.Office.Interop.Word;


namespace TagCloud.WordsPreprocessing.DocumentParsers
{
    public class DocParser : IDocumentParser
    {
        public HashSet<string> AllowedTypes { get; }
        private MsWord.Application application;

        public DocParser()
        {
            AllowedTypes = new HashSet<string> {".doc", ".docx"};
        }

        public IEnumerable<string> GetWords(ApplicationSettings settings)
        {
            if (application is null)
            {
                application = new MsWord.Application { Visible = false };
                application.DocumentBeforeClose +=
                    (MsWord.Document a, ref bool b) => application = null;
            }

            if (application.Documents.Count == 0 || settings.FilePath != application.ActiveDocument.Path)
            {
                application.Documents.Open(settings.FilePath);
            }

            return application.ActiveDocument.Range().Text
                .Split(new[] {" ", "\r\n", "\n", "\r", "\t"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public void Close()
        {
            application?.Quit(SaveChanges:false);
        }
    }
}
