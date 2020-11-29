using System.Drawing;

namespace TagCloud.Infrastructure.Layout.Tokens
{
    public interface ITokenMeasurer<in TToken>
    {
        public Size GetSize(int priority, TToken token);
    }
}