using System.Drawing.Imaging;

namespace TagsCloudContainer
{
    public interface IVisualisator
    {
        public void Paint();
        public void Save(string path, string filename, ImageFormat format);
    }
}