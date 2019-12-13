using System.Drawing;

namespace TagsCloudApp.App
{
    public interface IInitialSettings
    {
        string InputFilePath { get; }
        string OutputFilePath { get; }
        Size ImageSize { get; }
        Font WordsFont { get; }
        Color WordsColor { get; }
    }
}
