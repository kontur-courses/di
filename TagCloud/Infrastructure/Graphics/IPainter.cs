using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public interface IPainter<TToken>
    {
        public Image GetImage(IEnumerable<(TToken, TokenInfo)> tokens);
    }
}