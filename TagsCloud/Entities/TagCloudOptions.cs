using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Colorizers;
using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Entities;

public class TagCloudOptions : ITagCloudOptions
{
    public CaseType        WordsCase       { get; set; }
    public bool            ToInfinitive    { get; set; }
    public HashSet<string> LanguageParts   { get; set; }
    public HashSet<string> ExcludedWords   { get; set; }
    public FontFamily      FontFamily      { get; set; }
    public ColorizerBase   Colorizer       { get; set; }
    public ILayout         Layout          { get; set; }
    public Color           BackgroundColor { get; set; }
    public Size            ImageSize       { get; set; }
}