namespace TagCloudDi.TextProcessing
{
    public class FileTextReader : ITextReader
    {
        public IEnumerable<string> GetWordsFrom(string filePath)
        {
            var words = new List<string>();
            using var sr = new StreamReader(filePath);
            var line = sr.ReadLine();
            while (line != null)
            {
                words.Add(line!.ToLower());
                line = sr.ReadLine();
            }

            return words;
        }
    }
}
