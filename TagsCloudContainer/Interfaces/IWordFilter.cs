namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        public List<string> FilterWords(List<string> taggedWords, List<string> boringWords,
            ICustomOptions options);
    }
}