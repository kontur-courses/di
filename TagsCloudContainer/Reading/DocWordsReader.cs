using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Reading

{
    public class DocWordsReader : IWordsReader
    {
        public List<string> ReadWords(string path)
        {
            Xceed.Words.NET.DocX doc = Xceed.Words.NET.DocX.Load(path);
            var regex = new Regex("\\W?(\\w+)\\W");
            var matches = regex.Matches(doc.Text);
            var res = new List<string>();
            foreach (Match match in matches)
            {
                res.Add(match.Groups[1].Value);
            }

            return res;
        }
    }
}