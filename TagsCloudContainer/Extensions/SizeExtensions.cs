using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class SizeExtensions
    {
        public static int GetArea(this Size size)
        {
            return size.Height * size.Width;
        }
    }
}