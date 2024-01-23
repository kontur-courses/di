namespace TagsCloudContainer.Algorithm
{
    public class FileParser : IFileParser
    {
        public List<string> ReadWordsInFile(string filePath)
        {
            var words = new List<string>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var lineWords = line.ToLower().Trim().Split(' ');
                if (lineWords.Length > 1)
                {
                    throw new Exception("There is more than one word in a line");
                }
                words.Add(lineWords[0]);
            }
            
            return words;
        }

    }
}
