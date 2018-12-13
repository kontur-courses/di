namespace TagCloud.Core.Settings.Interfaces
{
    public interface ITagCloudSettings
    {
        string PathToWords { get; set; }
        string PathToBoringWords { get; set; }
        string PathForResultImage { get; set; }
    }
}