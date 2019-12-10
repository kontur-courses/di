using System.Drawing;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.PaintersWords
{
    public interface IPainterWords
    {
        Brush GetNextBrush(WordToken wordToken);
    }
}
