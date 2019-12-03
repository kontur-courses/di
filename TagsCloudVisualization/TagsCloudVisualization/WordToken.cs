namespace TagsCloudVisualization
{
    public class WordToken
    {
        public readonly string Word;
        public readonly int TextCount;

        public WordToken(string word, int textCount)
        {
            Word = word;
            TextCount = textCount;
        }
    }
}