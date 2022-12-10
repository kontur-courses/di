using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer
{
    public class TextReader
    {
        public static string GetTextFromFile(string path)
        {
            var builder = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    builder.Append(line + Environment.NewLine);
                }
            }

            return builder.ToString();
        }
    }
}