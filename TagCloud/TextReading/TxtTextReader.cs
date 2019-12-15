using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public class TxtTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            try
            {
                return File.ReadLines(filePath);
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException($"File {filePath} doesn't exist", e);
            }
            catch (DirectoryNotFoundException e)
            {
                throw new DirectoryNotFoundException($"Some part of path {filePath} doesn't exist", e);
            }
            
        }
    }
}
