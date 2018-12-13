using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public interface IColorManager
    {
        Dictionary<TagsCloudWord, Color> GetWordsColors(List<TagsCloudWord> words);
    }
}