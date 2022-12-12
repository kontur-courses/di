namespace TagsCloud2.TagsCloudMaker.SizeDefiner;

public interface ISizeDefiner
{
    public Dictionary<string, TextOptions> DefineStringSizeAndOrientation(
        Dictionary<string, int> stingAndFontSize, bool withVerticalWords, string fontFamilyName);
}