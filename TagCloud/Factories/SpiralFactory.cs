using TagCloud.CloudLayouter.FigurePaths;

namespace TagCloud.Factories
{
    public class SpiralFactory : IFigurePathFactory
    {
        public double DistanceBetweenTurns { get; set; }
        public double DeltaAngle { get; set; }

        public SpiralFactory()
        {
            DistanceBetweenTurns = 10;
            DeltaAngle = 1.5;
        }

        public IFigurePath GetFigurePath()
        {
            return new Spiral(DistanceBetweenTurns, DeltaAngle);
        }
    }

    public interface IFigurePathFactory
    {
        IFigurePath GetFigurePath();
    }
}
