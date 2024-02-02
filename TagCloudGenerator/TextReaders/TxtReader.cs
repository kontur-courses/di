namespace TagCloudGenerator.TextReaders
{
    public class TxtReader : ITextReader
    {
        public bool IsFileExtension(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            return extension == ".txt";
        }

        public IEnumerable<string> ReadTextFromFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}