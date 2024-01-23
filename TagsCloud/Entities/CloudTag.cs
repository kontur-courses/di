using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Numerics;

namespace TagsCloud.Entities;

public class CloudTag
{
    public string? InnerText { get; set; }
    public Font? TextFont { get; set; }
    public Color TextColor { get; set; }
    public Vector2 Location { get; set; }
}