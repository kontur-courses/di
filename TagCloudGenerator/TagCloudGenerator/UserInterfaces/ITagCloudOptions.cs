namespace TagCloudGenerator.UserInterfaces
{
    public interface ITagCloudOptions
    {
        string CloudVocabularyFilename { get; }
        string ImageSize { get; }
        string ExcludedWordsVocabularyFilename { get; }
        string ImageFilename { get; }
        int GroupsCount { get; }
        string MutualFont { get; }
        string BackgroundColor { get; }
        string FontSizes { get; }
        string TagColors { get; }
    }
}