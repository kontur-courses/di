using TagCloudPainter.Common;

namespace TagCloudPainter.Interfaces;

public interface IParseSettingsProvider
{
    ParseSettings ParseSettings { get; }
}