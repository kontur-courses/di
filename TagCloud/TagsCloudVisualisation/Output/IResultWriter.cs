using System.Drawing;

namespace TagsCloudVisualisation.Output
{
    public interface IResultWriter
    {
        void Save(Image result);
    }
}