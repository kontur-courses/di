using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Words.NET;

namespace TagCloud.Readers
{
    public class DocFileReader : IFileReader
    {
        public string[] ReadFile(string filename)
        {
            var doc = DocX.Load(filename);
            //var text = new List<string>();
            return doc.Paragraphs.Select(p => p.Text).ToArray();
            //foreach (var p in doc.Paragraphs)
            //{
            //    text.Add(p.Text);
            //}
        }
    }
}
