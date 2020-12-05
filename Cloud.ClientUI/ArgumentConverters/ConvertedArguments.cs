using System.Collections.Generic;
using System.Drawing;
using CloudContainer;

namespace Cloud.ClientUI.ArgumentConverters
{
    public class ConvertedArguments : IArguments
    {
        public Font Font { get; set; }
        public Size ImageSize { get; set; }
        public Point Center { get; set; }
        public Color TextColor { get; set; }

        public HashSet<string> BoringWords { get; set; }

        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }
    }
}