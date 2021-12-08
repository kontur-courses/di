using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordCloudPainter
    {
        public Bitmap PaintWords(ImageSettings imageSettings);
    }
}