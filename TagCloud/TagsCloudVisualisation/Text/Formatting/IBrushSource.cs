using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public interface IBrushSource
    {
        Color BackgroundColor { get; }
        Brush GetBrush(string word, double distanceFromCenter);
    }
}