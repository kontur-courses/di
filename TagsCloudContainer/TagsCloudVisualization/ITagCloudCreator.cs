using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagCloudCreator
    {
        public Bitmap CreateFromFile(string filepath);
    }
}