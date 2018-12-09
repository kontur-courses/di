using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> Read(string path)
        {
            IEnumerable<string> result;
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                var regex = new Regex("[^\\w]?(\\w+)[^\\w]?");
                var content = sr.ReadToEnd();
                result = regex
                    .Matches(content)
                    .Cast<Match>()
                    .Select(m => m.Groups[1].Value);
            }

            return result;
        }
    }
}