using System.Drawing;

namespace TagCloud
{
    public interface ICurve
    {
        Point CurrentPoint { get; }
        void Next();
    }
}