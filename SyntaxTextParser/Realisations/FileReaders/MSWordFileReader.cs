using Microsoft.Office.Interop.Word;
using SyntaxTextParser.Architecture;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SyntaxTextParser
{
    public class MsWordFileReader : IFileReader
    {
        public bool TryReadText(string filePath, out string text)
        {
            object refFullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            var none = Type.Missing;
            var app = new Application();
            try
            {
                #region application.Documents.Open(refFilePath)
                app.Documents.Open(ref refFullFilePath,
                    ref none, ref none, ref none, ref none,
                    ref none, ref none, ref none, ref none,
                    ref none, ref none, ref none, ref none, ref none,
                    ref none, ref none);
                #endregion

                var document = app.Documents.Application.ActiveDocument;
                object startIndex = 0;
                object endIndex = document.Characters.Count;
                var docRange = document.Range(ref startIndex, ref endIndex);
                
                text = docRange.Text;
                app.Quit(ref none, ref none, ref none);

                return true;
            }
            catch (COMException)
            {
                text = null;
                return false;
            }
        }

        public bool CanReadThatType(string type)
        {
            return type == "doc" || type == "docx";
        }
    }
}