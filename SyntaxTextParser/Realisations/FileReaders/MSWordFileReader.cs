using Microsoft.Office.Interop.Word;
using SyntaxTextParser.Architecture;
using System;
using System.Runtime.InteropServices;

namespace SyntaxTextParser
{
    public class MSWordFileReader : IFileReader
    {
        public bool TryReadText(string filePath, out string text)
        {
            object refFilePath = filePath;
            var none = Type.Missing;
            var app = new Application();
            try
            {
                #region application.Documents.Open(refFilePath)
                app.Documents.Open(ref refFilePath,
                    ref none, ref none, ref none, ref none,
                    ref none, ref none, ref none, ref none,
                    ref none, ref none, ref none, ref none, ref none,
                    ref none, ref none);
                #endregion

                var document = app.Documents.Application.ActiveDocument;
                object startIndex = 0;
                object endIndex = document.Characters.Count;
                var docRange = document.Range(ref startIndex, ref endIndex);
                app.Quit(ref none, ref none, ref none);
                
                text = docRange.Text;
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