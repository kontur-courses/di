using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TagsCloudContainer.WordsReaders
{
    public class TxtReader : IWordsReader
    {
        public IEnumerable<string> GetWords(string filename)
        {
            using (var reader = new StreamReader(filename, Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}