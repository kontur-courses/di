using System.Drawing;

namespace TagsCloudVisualisation.Output
{
    public interface IFileResultWriter
    {
        void Save(Image result, string path);
    }
}