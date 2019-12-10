namespace TagsCloudContainer.WordPreprocessors
{
    public interface IWordPreprocessor
    {
        string[] WordPreprocessing(string[] text);
    }
}
