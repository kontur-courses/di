using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    /// <summary>
    /// This theme uses godot engine colors
    /// </summary>
    public class GodotEngineTheme : ITheme
    {
        public string[] TextColors => new[]
        {
            "#BCC2C5",
            "#BCF1E0",
            "#F4E29A",
            "#6CB3F8",
            "#FB6F83"
        };

        public string BackgroundColor => "#1D2630";
    }
}