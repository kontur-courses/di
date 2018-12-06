using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public interface IGraphicsHolder
    {
        void Save();
        void Draw(Rectangle rectangle, string word);
    }
}