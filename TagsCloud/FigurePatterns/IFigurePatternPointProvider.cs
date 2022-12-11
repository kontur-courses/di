using System.Drawing;

namespace TagsCloud.FigurePatterns
{
    public interface IFigurePatternPointProvider
    {
        Point GetNextPoint();
        void Restart();
    }
}