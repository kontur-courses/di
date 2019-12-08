using System.Drawing;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.PaintersWords
{
    interface IPainterWords
    {
        Brush GetNextBrush(WordToken wordToken);
    }
}
