using System.Collections.Generic;
using System.Drawing;

namespace CloudContainer
{
    public class ConvertedArguments
    {
        public Font font { get; set; }
        public Size imageSize { get; set; }
        public Point center { get; set; }
        public Color textColor { get; set; }

        public HashSet<string> BoringWords { get; set; }
    }
}