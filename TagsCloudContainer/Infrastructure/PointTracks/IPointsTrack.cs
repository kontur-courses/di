using System.Drawing;

namespace TagsCloudContainer.Infrastructure.PointTracks
{
    public interface IPointsTrack
    {
        Point GetNextPoint();
    }
}