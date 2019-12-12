using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloud.WordPreprocessing
{
    public class FileReader : IWordGetter
    {
        public readonly FileInfo FileName;

        public readonly Encoding Encoding;

        public FileReader(FileInfo fileName, Encoding encoding = null)
        {
            if (!fileName.Exists) throw new FileNotFoundException();
            FileName = fileName;
            Encoding = encoding ?? Encoding.Default;
        }

        public IEnumerable<string> GetWords(params char[] delimiters)
        {
            delimiters = delimiters.ToList().Append(' ').ToArray();
            using (var sr = new StreamReader(FileName.FullName, Encoding))
            {
                var regex = new Regex(@"^\s*$");
                return sr.ReadToEnd().Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => !regex.IsMatch(w));
            }
        }
    }
}