namespace TagsCloudContainer.Core.WordsParser.Interfaces
{
    public interface IWordsFilter
    {
        public HashSet<string> RemoveBoringWords(HashSet<string> words);
    }
}
