using System;
using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Settings
{
    public interface IWordsScaleSettings
    {
        Func<PointF, PointF, IScaler> GetScaler { get; set; }
    }

    public class DefaultWordsScaleSettings : IWordsScaleSettings
    {
        public Func<PointF, PointF, IScaler> GetScaler { get; set; } = (start, end) => new LinearScaler(start, end);
    }
}