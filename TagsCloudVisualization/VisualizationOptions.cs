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

    public List<string> BoringWords { get; set; }

    public List<string> ExcludedPartsOfSpeech { get; set; }
    public float MinFontSize { get; set; }
    public float MaxFontSize { get; set; }

    public string DirectoryToMyStemProgram { get; set; }

    public VisualizationOptions()
    {
        Palette = new Palette(Brushes.Black);
        BoringWords = new List<string>();
        ExcludedPartsOfSpeech = new List<string>();
    }
}