using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.FigurePaths;

namespace TagCloud.Factories
{
    class SpiralFactory : IFigurePathFactory
    {
        public IFigurePath GetFigurePath()
        {
            return new Spiral(100);
        }
    }

    public interface IFigurePathFactory
    {
        IFigurePath GetFigurePath();
    }
}
