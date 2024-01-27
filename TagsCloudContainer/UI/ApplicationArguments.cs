using TagsCloudContainer.utility;

namespace TagsCloudContainer.UI;

public class ApplicationArguments
{
    public void DefaultInit()
    {
        Format = "jpg";
        FontSize = 30;
        Resolution = [1920, 1080];
        Center = [960, 540];
        Background = [255, 255, 255];
        Scheme = [0, 0, 0, 255];
        Exclude = Utility.GetRelativeFilePath("src/boringWords.txt");
    }

    public string Input { get; private set; }
    public string Output { get; private set; }
    public string FontPath { get; private set; }
    public string Format { get; private set; }
    public int FontSize { get; private set; }
    public List<int> Resolution { get; private set; }
    public List<int> Center { get; private set; }
    public List<int> Background { get; private set; }
    public List<int> Scheme { get; private set; }
    public string Exclude { get; private set; }
}