using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Extensions;
using TagCloud.Layouters;
using TagCloud.Settings;
using TagCloud.Sources;
using TagCloud.TagClouds;

namespace TagCloud.Visualizers
{
    public class DistanceColorVisualizer : IVisualizer<CircleTagCloud>
    {
        private static readonly Graphics FakeGraphics = Graphics.FromImage(new Bitmap(1, 1));
        private readonly CloudSettings cloudSettings;
        private readonly Dictionary<Rectangle, string> rectangleToWord = new Dictionary<Rectangle, string>();

        public DistanceColorVisualizer(ISource source, ILayouter layouter, CircleTagCloud cloud, CloudSettings settings)
        {
            VisualizeTarget = cloud;
            cloudSettings = settings;
            foreach (var word in source.GetWordsWithWeight())
            {
                var size = GenerateSize(FakeGraphics, word.Key, word.Value);
                var rectangle = layouter.PutNextRectangle(size);
                cloud.AddElement(rectangle);
                rectangleToWord.Add(rectangle, word.Key);
            }
        }

        public CircleTagCloud VisualizeTarget { get; }

        public void Draw(Graphics graphics)
        {
            var leftUpBound = VisualizeTarget.LeftUpBound;
            graphics.TranslateTransform(-leftUpBound.X, -leftUpBound.Y);
            foreach (var rectangle in VisualizeTarget)
            {
                var distance = VisualizeTarget.Center.DistanceBetween(rectangle.Center());
                using var brush = GetBrush(LinearMath.LinearInterpolate(cloudSettings.InnerColorRadius,
                    cloudSettings.OuterColorRadius, distance));
                graphics.DrawString(rectangleToWord[rectangle], cloudSettings.Font, brush, rectangle);
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

        private Size GenerateSize(Graphics graphics, string word, double weight)
        {
            var size = graphics.MeasureString(word, cloudSettings.Font);
            return new Size(
                (int)Math.Ceiling(size.Width * weight),
                (int)Math.Ceiling(size.Height * weight));
        }
    }
}
