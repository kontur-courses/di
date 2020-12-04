namespace TagsCloudVisualisation.Text
{
    public interface IFileWordsReader
    {
        string[] GetWordsFrom(string path);
    }
}