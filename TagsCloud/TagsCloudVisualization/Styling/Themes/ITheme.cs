using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    public interface ITheme
    {
        string[] TextColors { get; }

        string BackgroundColor { get; }
    }
}