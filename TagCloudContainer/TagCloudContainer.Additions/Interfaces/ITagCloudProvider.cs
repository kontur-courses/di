using TagCloudContainer.Additions.Models;

namespace TagCloudContainer.Additions.Interfaces;

public interface ITagCloudProvider
{
    public IEnumerable<Word> GetPreparedWords();
}