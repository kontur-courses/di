using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainerCore.InterfacesCore;

public interface ITagMaker
{
    public float GetFontSize(KeyValuePair<string, int> tag, IStatisticMaker maker, float maxFontSize);
    public Size GetTagSize(string tag, string fontName, float fontSize, Size imageSize);
}