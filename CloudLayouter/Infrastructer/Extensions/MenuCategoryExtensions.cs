using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Infrastructer.Extensions
{
    public static class MenuCategoryExtensions
    {
        public static string GetDescription(this MenuCategory menuCategory)
        {
            return menuCategory.ToString();
        }
    }
}