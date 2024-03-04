namespace TagCloudGenerator.TextReaders
{
    public class TxtReader : ITextReader
    {
        public string GetFileExtension() => ".txt";
        
        public IEnumerable<string> ReadTextFromFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}