namespace TagsCloudVisualizationConsole;

public class ArgsOptions
{
    public string PathToTextFile { get; set; }
    public string DirectoryToSaveFile { get; set; }
    public string DirectoryToMyStemProgram { get; set; }
    public string SaveFileName { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
    public float SpiralStep { get; set; }
    public string BackgroundColor { get; set; }
    public string FontFamily { get; set; }
    public int TakeMostPopularWords { get; set; }
    public float MinFontSize { get; set; }
    public float MaxFontSize { get; set; }
    public string PaletteDefaultBrush { get; set; }
    public string FileExtension { get; set; }
    public List<string> PaletteAvailableBrushes { get; set; }
    public List<string> BoringWords { get; set; }
    public List<string> ExcludedPartsOfSpeech { get; set; }

    public ArgsOptions()
    {
        PaletteAvailableBrushes = new List<string>();
        BoringWords = new List<string>();
        ExcludedPartsOfSpeech = new List<string>();
    }
}