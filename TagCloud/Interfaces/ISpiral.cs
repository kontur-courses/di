using TagCloud.Layouter;

namespace TagCloud.Interfaces
{
    public interface ISpiral
    {
        Point Put(Point origin, double angle, double turnsInterval);
    }
}