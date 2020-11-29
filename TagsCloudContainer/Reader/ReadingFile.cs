using System;
using System.IO;

namespace TagsCloudContainer.Reader
{
    public class ReadingFile
    {
        public string GetTextFromFile(string pathToFile)
        {
            try
            {
                using var reader = new StreamReader(pathToFile);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Can't read the file", e);
            }
        }
    }
}