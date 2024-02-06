
namespace TagsCloudContainer.Interfaces
{
    public interface IPreprocessor
    {
        IEnumerable<string> Process(IEnumerable<string> words, string boringWordsFilePath);
    }
}
