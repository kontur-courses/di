namespace TagsCloudGenerator.Interfaces
{
    public interface IPointsSearcher : IFactorial, IResettable
    {
        System.Drawing.PointF GetNextPoint();
    }
}