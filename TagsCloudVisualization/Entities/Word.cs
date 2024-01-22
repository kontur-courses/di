using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Numerics;

namespace TagsCloudVisualization.Entities;

public class Word
{
    public string Text { get; set; }
    public Font Font { get; set; }
    public Color Color { get; set; }
    public Vector2 Position { get; set; }
}