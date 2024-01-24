namespace TagCloudDi.TextProcessing
{
    public class TextReader
    {
        public IEnumerable<string> GetWordsFrom(string filePath)
        {
            var words = new List<string>();
            using var sr = new StreamReader(filePath);
            var line = sr.ReadLine();
            while (line != null)
            {
                line = sr.ReadLine();
                words.Add(line!.ToLower());
            }

            return words;
        }
    }
}
