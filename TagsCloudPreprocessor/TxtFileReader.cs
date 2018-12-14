using System.IO;
using Xceed.Words.NET;

namespace TagsCloudPreprocessor
{
    public class TxtFileReader:IFileReader
    {
        public string ReadFromFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}