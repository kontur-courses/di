using TagCloudContainer.Additions.Models;

namespace TagCloudContainer.Additions.Interfaces;

public interface ITagCloudPlacer
{
    public Word PlaceInCloud(Word word);
}