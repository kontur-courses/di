using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace TagsCloudContainer.Reading

{
    public class DocWordsReader : IWordsReader
    {
        public List<string> ReadWords(string path)
        {
            Application application = new Application();
            Document document = application.Documents.Open(path);

            var result = new List<string>();
            for (var i = 1; i <= document.Words.Count; i++)
            {
                result.Add(document.Words[i].Text);
            }

            application.Quit();
            return result;
        }
    }
}