using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public interface ICloudCreator
{
    Bitmap CreatePicture(FromFileStreamContext streamContext);

    void AddBoringWordManager(IBoringWords boringWordsManager);
}