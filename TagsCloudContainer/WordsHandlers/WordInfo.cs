namespace TagsCloudContainer.WordsHandlers
{
    public class WordInfo
    {
        public readonly string Word;
        public readonly int Repetitions;

        public WordInfo(string word, int repetitions)
        {
            Word = word;
            Repetitions = repetitions;
        }
    }
}