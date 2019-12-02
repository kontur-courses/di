namespace TagsCloudTextPreparation
{
    public class TagsCloudWord
    {
        public string Word { get; }
        public int TextOccurrence { get; }

        public TagsCloudWord(string word, int textOccurrences)
        {
            Word = word;
            TextOccurrence = textOccurrences;
        }
    }
}