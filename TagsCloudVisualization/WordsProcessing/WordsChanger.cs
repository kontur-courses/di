namespace TagsCloudVisualization
{
    public class WordsChanger : IWordsChanger
    {
        public string ChangeWord(string word)
        {
            return word.ToLower();
        }
    }
}