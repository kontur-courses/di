using System.Drawing;

namespace TagsCloudVisualization;

public class VisualizationOptions
{
    public Size CanvasSize { get; set; }
    public float SpiralStep { get; set; }
    public Color BackgroundColor { get; set; }
    public Palette Palette { get; set; }
    public FontFamily FontFamily { get; set; }
    public int TakeMostPopularWords { get; set; }

    public string TextsPath { get; set; }
    public string WordsFileName { get; set; }
    public List<string> BoringWordsName { get; set; }

    // public Color FontColor { get; set; }
    public string ExcludedPartsOfSpeech { get; set; }
    public float MinFontSize { get; set; }
    public float MaxFontSize { get; set; }
}