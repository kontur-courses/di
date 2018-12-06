using System;
using System.IO;

namespace TagCloud
{
    public class TxtTextReader : ITextReader
    {
        private readonly string fileName;

        public TxtTextReader(string fileName)
        {
            this.fileName = fileName;
            
        }

        public string TryReadText()
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                byte[] fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                return System.Text.Encoding.Default.GetString(fileBytes);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
                return null;
            }
            finally
            {
                fileStream?.Close();
            }
        }
    }
}