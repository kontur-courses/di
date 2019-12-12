using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace TagsCloudContainer.Reader
{
    public class ReaderFromTxt : IReader
    {
        private readonly string path;

        public ReaderFromTxt(string path)
        {
            this.path = path;
        }
        
        public IEnumerable<string> GetWorldSet()
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