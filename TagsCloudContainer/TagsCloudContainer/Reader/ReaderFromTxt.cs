using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Reader
{
    public class ReaderFromTxt : IReader
    {
        public ReaderFromTxt()
        {
            
        }
        public IEnumerable<string> GetWorldSet(string path)
        {
            using (var sr = new StreamReader(path))
            {
                string strLine;
                while ((strLine = sr.ReadLine()) != null)
                    yield return strLine.Trim();
            }
        }
    }
}