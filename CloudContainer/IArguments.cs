using System.Collections.Generic;
using System.Drawing;

namespace CloudContainer
{
    public interface IArguments
    {
        public Font Font { get; }
        public Size ImageSize { get; }
        public Point Center { get; }
        public Color TextColor { get; }

        public HashSet<string> BoringWords { get; }

        public string InputFileName { get; }
        public string OutputFileName { get; }
    }
}