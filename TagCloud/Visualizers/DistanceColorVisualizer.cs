using System.Drawing;
using TagCloud.Extensions;
using TagCloud.Settings;
using TagCloud.TagClouds;

namespace TagCloud.Visualizers
{
    public class DistanceColorVisualizer : IVisualizer<CircleTagCloud>
    {
        private readonly CloudSettings cloudSettings;

        public DistanceColorVisualizer(CircleTagCloud cloud, CloudSettings settings)
        {
            VisualizeTarget = cloud;
            cloudSettings = settings;
        }

        public CircleTagCloud VisualizeTarget { get; }

        public void Draw(Graphics graphics)
        {
            var leftUpBound = VisualizeTarget.LeftUpBound;
            graphics.TranslateTransform(-leftUpBound.X, -leftUpBound.Y);
            foreach (var rectangle in VisualizeTarget)
            {
                var distance = VisualizeTarget.Center.DistanceBetween(rectangle.Center());
                using var brush = GetBrush(LinearMath.LinearInterpolate(cloudSettings.InnerColorRadius, cloudSettings.OuterColorRadius, distance));
                graphics.FillRectangle(brush, rectangle);
            }
        }

        private SolidBrush GetBrush(double gradientBlend)
        {
            var a = GetColorComponent(cloudSettings.InnerColor.A, cloudSettings.OuterColor.A, gradientBlend);
            var r = GetColorComponent(cloudSettings.InnerColor.R, cloudSettings.OuterColor.R, gradientBlend);
            var g = GetColorComponent(cloudSettings.InnerColor.G, cloudSettings.OuterColor.G, gradientBlend);
            var b = GetColorComponent(cloudSettings.InnerColor.B, cloudSettings.OuterColor.B, gradientBlend);
            return new SolidBrush(Color.FromArgb(a, r, g, b));
        }

        private int GetColorComponent(int from, int to, double blend)
        {
            return (int)((to - from) * blend + from);
        }
    }
}
