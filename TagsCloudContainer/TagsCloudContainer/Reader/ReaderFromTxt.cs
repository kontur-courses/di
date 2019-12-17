using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Reader
{
    public class ReaderFromTxt : IReader
    {
        public IEnumerable<string> GetWordsSet(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                string strLine;
                while ((strLine = streamReader.ReadLine()) != null)
                    yield return strLine.Trim();
            }
        }
    }
}