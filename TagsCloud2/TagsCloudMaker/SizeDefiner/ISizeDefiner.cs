namespace TagsCloud2;

public interface ISizeDefiner
{
    public Dictionary<string, TextOptions> DefineStringSizeAndOrientation(
        Dictionary<string, int> stingAndFontSize, bool withVerticalWords, string fontFamilyName);
}