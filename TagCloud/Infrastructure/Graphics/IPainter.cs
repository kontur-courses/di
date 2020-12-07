using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public interface IPainter
    {
        public Image GetImage(IEnumerable<(string, TokenInfo)> tokens);
    }
}