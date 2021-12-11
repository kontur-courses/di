namespace TagsCloudContainer.Settings
{
    public interface IAppSettings
    {
        int ImageHeight { get; }
        int ImageWidth { get; }
        string FontName { get; }
        string FontColorName { get; }
        string BackgroundColorName { get; }
        string ImagePath { get; }
        string InputPath { get; }
        string BoringWordsPath { get; }
        double AngleStep { get; }
        double Density { get; }
    }
}