using System.Drawing;

namespace TagsCloudVisualization.Common.Settings
{
    public interface ITagStyleSettings
    {
        public Color[] ForegroundColors { get; set; }
        public string[] FontFamilies { get; set; }
        public float Size { get; set; }
        public float SizeScatter { get; set; }
    }
}