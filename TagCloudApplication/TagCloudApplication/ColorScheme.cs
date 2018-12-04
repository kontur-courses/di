using System.Drawing;

namespace TagCloudApplication
{
    public interface IColorScheme
    {
        Color BackColor { get; }
        Color GetNextColorForWord();
    }
}