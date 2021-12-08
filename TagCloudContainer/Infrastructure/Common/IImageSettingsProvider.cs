namespace TagCloud.Infrastructure.Common;

public interface IImageSettingsProvider
{
    int ImageWidth { get; }
    int ImageHeight { get; }
}