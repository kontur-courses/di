using System.Drawing;

namespace TagsCloud2.TagsCloudMakerManager;

public interface ITagsCloudMakerManager
{
    public void MakeTagsCloud(string path,
        string fontFamilyName,
        Brush colorBrush,
        string pathToSave,
        string formatToSave,
        bool isVerticalWords,
        int size,
        string pathToExcludingPaths);
}