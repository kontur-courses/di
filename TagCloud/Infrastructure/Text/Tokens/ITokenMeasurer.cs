using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Infrastructure.Text.Tokens
{
    public interface ITokenMeasurer<TToken>
    {
        public Dictionary<TToken, Size> GetSizes(Dictionary<TToken, int> tokenFontSizes);
    }
}