using TagsCloudContainer.utility;

namespace TagsCloudContainer.UI;

public class ApplicationArguments
{
    public string Input { get; set; } = null!;
    public string Output { get; set; } = null!;
    public string FontPath { get; set; } = null!;
    public string Format { get; set; } = "jpg";
    public int FontSize { get; set; } = 30;
    public List<int> Resolution { get; set; } = [1920, 1080];
    public List<int> Center { get; set; } = [960, 540];
    public List<int> Background { get; set; } = [255, 255, 255];
    public List<int> Scheme { get; set; } = [0, 0, 0, 255];
    public string Exclude { get; set; } = Utility.GetAbsoluteFilePath("src/boringWords.txt");
}