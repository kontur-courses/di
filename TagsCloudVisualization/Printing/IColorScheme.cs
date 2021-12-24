using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsCloudVisualizationTest")]
namespace TagsCloudVisualization.Printing
{
    public interface IColorScheme
    {
        Color GetColorBy(Size size);
    }
}