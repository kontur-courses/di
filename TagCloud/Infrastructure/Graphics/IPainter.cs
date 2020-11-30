using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout;

namespace TagCloud.Infrastructure.Graphics
{
    public interface IPainter<TToken>
    {
        public Image GetImage(Dictionary<TToken, Size> sizedTokens, Dictionary<TToken, int> tokenFontSizes);
    }
}