using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.InputStream;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public interface ICloudCreator
{
    Bitmap CreatePicture(IStreamContext streamContext);
}