using System.Drawing;

namespace TagCloud.FileSaver;

public interface ISaver
{
    void Save(Bitmap bitmap, string OutputPath, string imageFormat);
}