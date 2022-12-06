using System.Drawing;

namespace TagsCloud.FigurePatterns
{
    internal interface IFigurePatternPointProvider
    {
        Point GetNextPoint();
        void Restart();
    }
}