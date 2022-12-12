using System;
using System.IO;
using System.Text;

namespace TagsCloudContainer.TextReaders
{
    public class TxtReader : ITextReader
    {
        public string GetTextFromFile(string path)
        {
            var builder = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    builder.Append(line + Environment.NewLine);
                    line = sr.ReadLine();
                }
            }

            return builder.ToString();
        }
    }
}