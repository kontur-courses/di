using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.Visualizers
{
    public interface IVisualizer
    {
        Bitmap VisualizeCloud(List<WordToken> wordTokens);
    }
}
