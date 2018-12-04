using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.WordPrework
{
    public class FileReader : IWordsGetter
    {
        public readonly string FileName;

        public readonly Encoding Encoding;

        public FileReader(string fileName, Encoding encoding = null)
        {
            FileName = fileName;
            Encoding = encoding ?? Encoding.Default;
        }

        public IEnumerable<string> GetWords(params char[] delimiters)
        {
            using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (delimiters.Length == 0)
                        yield return line;
                    else
                        foreach (var word in line.Split(delimiters))
                            yield return word;
                }
            }
        }
    }
}
