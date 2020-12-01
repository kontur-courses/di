using System;
using System.IO;

namespace TagsCloudContainer.Reader
{
    public static class ReadingFile
    {
        public static string GetTextFromFile(string pathToFile)
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