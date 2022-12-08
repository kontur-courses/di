namespace MyStemWrapper.Domain.Settings;

public class MyStemSettings
{
    public string MyStemAppPath { get; set; } = null!;
    public ICollection<MyStemLaunchOption>? LaunchOptions { get; set; }
    public MyStemOutputFormat OutputFormat { get; set; } = MyStemOutputFormat.Text;
    public MyStemEncoding Encoding { get; set; } = MyStemEncoding.Utf8;
}