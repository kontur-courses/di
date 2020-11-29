using System.Drawing;

namespace TagCloud.Infrastructure.Layout.Tokens
{
    public interface ITokenMeasurer<in TToken>
    {
        public SizeF GetSize(int priority, TToken token);
    }
}