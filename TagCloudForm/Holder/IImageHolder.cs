using System.Drawing;
using TagCloud.Visualization;

namespace TagCloudForm.Holder
{
    public interface IImageHolder
    {
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);

        Image Image { set; }
    }
}