using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public interface ICloudCreator
{
    Bitmap CreatePicture(FromFileStreamContext streamContext);
}