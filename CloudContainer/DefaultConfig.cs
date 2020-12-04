using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudContainer
{
    public static class DefaultConfig
    {
        public static Point GetCenter()
        {
            return new Point(1500, 1500);
        }

        public static Size GetSize()
        {
            return new Size(1500, 1500);
        }

        public static Font GetFont()
        {
            return new Font(FontFamily.GenericMonospace, 25);
        }

        public static Color GetColor()
        {
            return Color.Blue;
        }

        public static HashSet<string> GetBoringWords()
        {
            return new List<string>
            {
                "в", "без", "до", "для", "за", "через", "над", "по", "из", "у", "около",
                "под", "о", "про", "на", "к", "перед", "при", "с", "между"
            }.ToHashSet();
        }
    }
}