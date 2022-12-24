using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Interfaces;

public interface ITagCloudPlacer
{
    public Result<Word> PlaceInCloud(Word word);
}