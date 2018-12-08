using System;
using System.IO;

namespace TagsCloudPreprocessor
{
    public class TxtReader:IReader
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
