using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Interfaces;

public interface ITagCloudProvider
{
    public Result<List<Word>> GetPreparedWords();
}