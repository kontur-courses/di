using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Savers
{
    public interface IImageSaver
    {
        IEnumerable<string> Extensions { get; }
        void Save(string path, string extension, Image image);
    }
}