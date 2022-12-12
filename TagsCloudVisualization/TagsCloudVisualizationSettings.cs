using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.FontSettings;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.ImageSettings;

namespace TagsCloudVisualization;

public class TagsCloudVisualizationSettings
{
    public string Filepath { get; set; }
    
    public string OutputDirectory { get; set; }

    public IImageSettingsProvider ImageSettingsProvider { get; set; }

    public IFontSettingsProvider FontSettingsProvider { get; set; }

    public ICloudLayouter CloudLayouter { get; set; }

    public IColorGenerator ColorGenerator { get; set; }

    public AbstractImageSaver ImageSaver { get; set; }
    
    public IReadOnlyCollection<string> BoringWords { get; set; } = Array.Empty<string>();
    
    public int TagCount { get; set; }
}