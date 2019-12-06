using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloud.WordPreprocessing
{
    public class FileReader : IWordGetter
    {
        public readonly FileInfo FileName;

        public readonly Encoding Encoding;

        public FileReader(FileInfo fileName, Encoding encoding = null)
        {
            if(!fileName.Exists) throw new FileNotFoundException();
            FileName = fileName;
            Encoding = encoding ?? Encoding.Default;
        }

        public IEnumerable<string> GetWords(params char[] delimiters)
        {
            delimiters = delimiters.Length > 0 ? delimiters : new char[] {'\n'};
            using (var sr = new StreamReader(FileName.FullName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var word in line.Split(delimiters).Where(w => w != "" && w != " "))
                        yield return word;
                }
            }
        }
    }
}