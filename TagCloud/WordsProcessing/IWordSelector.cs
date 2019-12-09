using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public interface IWordSelector
    {
        bool IsSelectedWord(Word word);
    }
}
