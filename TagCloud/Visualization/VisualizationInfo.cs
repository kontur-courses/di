using System.Drawing;
using System;

namespace TagCloud.Visualization
{
    internal class VisualizationInfo
    {
        private readonly Size? size;
        private readonly string font;
        private readonly Color? color;
        private readonly Random random = new Random();

        internal VisualizationInfo(Size? size = null, string font = null, Color? color = null)
        {
            this.size = size;
            this.font = Fonts.GetFont(font);
            this.color = color;
        }

        internal bool TryGetSize(out Size size)
        {
            size = this.size ?? Size.Empty;
            return this.size.HasValue;
        }

        internal Font GetFont(int emSize) => new Font(font, emSize);
        
        internal Color GetColor() => color ?? GetRandomColor();
        
        internal SolidBrush GetSolidBrush() => new SolidBrush(GetColor());

        private Color GetRandomColor() => Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
    }
}
