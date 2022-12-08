namespace TagsCloudContainer.Core.WordsParser.Interfaces
{
    public interface IFilters
    {
        public HashSet<string> RemoveBoringWords(HashSet<string> words);
    }
}
