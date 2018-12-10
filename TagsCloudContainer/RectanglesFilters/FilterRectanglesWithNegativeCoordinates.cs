using System.Drawing;
using TagsCloudContainer.WordsFilters;

namespace TagsCloudContainer.RectanglesFilters
{
    public class FilterRectanglesWithNegativeCoordinates : IFilter<Rectangle>
    {
        public bool IsCorrect(Rectangle rectangle)
            => rectangle.Left >= 0 && rectangle.Top >= 0;
    }
}