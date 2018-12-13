using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Reading

{
    public class DocWordsReader : IWordsReader
    {
        public List<string> ReadWords(string path)
        {
            Application application = new Application();
            Document document = application.Documents.Open(path);

            var result = new List<string>();
            var rx = new Regex("(\\w+)(' '+)?");

            for (var i = 1; i <= document.Words.Count; i++)
            {
                var word = rx.Match(document.Words[i].Text).Groups[0].Value;
                if (word != "")
                    result.Add(word);
            }

            application.Quit();
            return result;
        }
    }
}