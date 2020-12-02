namespace TagsCloud.TextProcessing.TextReaders
{
    public interface IReadersFactory
    {
        IWordsReader CreateReader();
    }
}
