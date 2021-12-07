using System;
using System.Drawing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Layout
{
    public class ScalersFactory : IScalersFactory
    {
        public IScaler GetScaler(PointF start, PointF end) => scalerFactory(start, end);

        private readonly Func<PointF, PointF, IScaler> scalerFactory;

        public ScalersFactory(Func<PointF, PointF, IScaler> scalerFactory)
        {
            this.scalerFactory = scalerFactory;
        }
    }
}