using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.WordKeepers
{
    public class TXTReader : IReader
    {
        public string GetText(string fileName)
        {
            return ReadTextFile(fileName, Encoding.Default);
        }

        public string GetTextWithEncoding(string fileName, Encoding encoding)
        {
            return ReadTextFile(fileName, encoding);
        }

        private static string ReadTextFile(string fileName, Encoding encoding)
        {
            string resultString;
            using (var fr = File.OpenRead(fileName))
            {
                var bytes = new byte[fr.Length];
                fr.Read(bytes, 0, bytes.Length);
                resultString = encoding.GetString(bytes);
            }

            return resultString;
        }
    }
}
