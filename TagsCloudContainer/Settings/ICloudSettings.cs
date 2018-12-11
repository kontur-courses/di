using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface ICloudSettings
    {
        int WordsToDisplay { get; set; }
        Point CenterPoint { get; }
        Size Size { get; set; }
    }
}
