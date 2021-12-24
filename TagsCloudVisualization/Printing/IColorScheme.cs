using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsCloudVisualizationTest")]
namespace TagsCloudVisualization.Printing
{
    internal interface IColorScheme
    {
        Color GetColorBy(Size size);
    }
}