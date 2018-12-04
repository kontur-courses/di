namespace TagsCloudContainer
{
    public interface IWordsReader
    {
        IWordStorage ReadWords(string path, IWordStorage wordStorage);
    }
}
