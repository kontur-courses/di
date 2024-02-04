namespace TagsCloudContainer.TextTools
{
    public class TextFileReader : ITextReader
    {
        public string ReadText(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                return reader.ReadToEnd();
            }
            catch
            {
                throw new FileNotFoundException("File {0} not found!", filePath);
            }
        }
    }
}
