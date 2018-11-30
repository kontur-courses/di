using System;
using Microsoft.Office.Interop.Word;

namespace TagsCloudContainer
{
    public class DocReader:IReader
    {
        public string ReadFromFile(string path)
        {
            var app = new Application {Visible = true};
            var obj = (object) (Environment.CurrentDirectory + "\\" + path);
            var doc = app.Documents.Open(ref obj);

            return doc.Content.Text;
        }
    }
}
