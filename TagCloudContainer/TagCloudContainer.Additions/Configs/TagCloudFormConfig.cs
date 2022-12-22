using TagCloudContainer.Additions.Interfaces;
using TagCloudContainer.Additions.Models;

namespace TagCloudContainer;

public class TagCloudFormConfig : ITagCloudFormConfig
{
    public string FontFamily { get; set; } = "Arial";
    public Color Color { get; set; } = Colors.GetAll().First().Value;
    public Color BackgroundColor { get; set; } = Colors.GetAll().First().Value;
    public Size FormSize { get; set; } = new Size(int.Parse(Screens.Sizes.First().Split("x")[0]), 
        int.Parse(Screens.Sizes.First().Split("x")[1]));
}