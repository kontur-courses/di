using TagCloudPainter.Common;

namespace TagCloudPainter.Interfaces;

public interface IImageSettingsProvider
{
    ImageSettings ImageSettings { get; }
}