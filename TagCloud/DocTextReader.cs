using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace TagCloud
{
    public class DocTextReader : ITextReader
    {
        public string TryReadText(string fileName)
        {
            object filename = fileName;
            object missed = Type.Missing;
            var application = new Application();
            try
            {
                application.Documents.Open(ref filename,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed,
                    ref missed);
                var document = application.Documents.Application.ActiveDocument;
                object start = 0;
                object stop = document.Characters.Count;
                var rng = document.Range(ref start, ref stop);
                var text = rng.Text;
                application.Quit(ref missed, ref missed, ref missed);
                return text;
            }
            catch (COMException)
            {
                Console.WriteLine("File not found. Write full name to file.");
                return null;
            }
            
        }
    }
}