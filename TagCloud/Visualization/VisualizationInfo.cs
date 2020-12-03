using System.Drawing;
using System;
using System.Linq;

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

        internal static Color? ReadColor(string colorRGB)
        {
            try
            {
                var result = ParseString(colorRGB);
                if (result.Length != 3)
                    return null;
                if (result.Any(i => i < 0 || i > 255))
                    return null;
                return Color.FromArgb(255, result[0], result[1], result[2]);
            }
            catch
            {
                return null;
            }
        }

        internal static Size? ReadSize(string sizeStr)
        {
            try
            {
                var result = ParseString(sizeStr);
                if (result.Length != 2)
                    return null;
                if (result.Any(i => i < 0))
                    return null;
                return new Size(result[0], result[1]);
            }
            catch
            {
                return null;
            }
        }

        private static int[] ParseString(string str) =>
            str.Split(' ')
            .Where(s => s != string.Empty)
            .Select(s => int.Parse(s))
            .ToArray();

        internal bool TryGetSize(out Size size)
        {
            size = this.size ?? Size.Empty;
            return this.size != null;
        }

        internal Font GetFont(int emSize) => new Font(font, emSize);
        
        internal Color GetColor() => color ?? GetRandomColor();
        
        internal SolidBrush GetSolidBrush() => new SolidBrush(GetColor());

        private Color GetRandomColor() => Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
    }
}
