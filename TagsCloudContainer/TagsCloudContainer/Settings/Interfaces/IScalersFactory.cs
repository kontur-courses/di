using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Settings.Interfaces
{
    public interface IScalersFactory
    {
        IScaler GetScaler(PointF start, PointF end);
    }
}