using System.Drawing;

namespace TagCloud
{
    public interface ISpiral
    {
        void Next();
        Point CurrentPoint { get; }
    }
}