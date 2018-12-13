using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualisation.Coloring
{
    public interface IColorManager
    {
        Dictionary<TagsCloudWord, Color> GetWordsColors(List<TagsCloudWord> words);
    }
}