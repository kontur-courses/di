using System.Drawing;

namespace TagsCloudVisualization;

public class Palette
{
    public List<Brush> AvailableBrushes { get; set; }

    public Brush DefaultBrush { get; set; }

    public Palette(Brush defaultBrush)
    {
        AvailableBrushes = new List<Brush>();
        DefaultBrush = defaultBrush;
    }


    public Palette(List<Brush> availableBrushes)
    {
        AvailableBrushes = availableBrushes;
    }
}