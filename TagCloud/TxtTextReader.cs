using System;
using System.IO;

namespace TagCloud
{
    public class TxtTextReader : ITextReader
    {
        public string TryReadText(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    var fileBytes = new byte[fileStream.Length];
                    fileStream.Read(fileBytes, 0, fileBytes.Length);
                    return System.Text.Encoding.Default.GetString(fileBytes);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found");
                    return null;
                }
            }
        }
    }
}