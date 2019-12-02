using System.Drawing;
using System.IO;

namespace TagsCloudContainer
{
    public class Setting
    {
        public Font Font { get; } = new Font("Arial", 8);
        public Brush Brush { get; } = Brushes.Black;
        public string Path { get; } = "timeSolution.txt";
    }
}