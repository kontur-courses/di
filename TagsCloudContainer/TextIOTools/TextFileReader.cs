namespace TagsCloudContainer.TextTools
{
    public class TextFileReader : ITextReader
    {
        public string ReadText(string filePath)
        {
            //try catch?
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
