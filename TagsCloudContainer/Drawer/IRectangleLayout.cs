using System.Collections.Generic;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public interface IRectangleLayout
    {
        public IEnumerable<WordRectangle> Rectangles { get; }

        public void DrawLayout();

        public void SaveLayout();

        public void PlaceWords(Dictionary<string, int> words);
    }
}