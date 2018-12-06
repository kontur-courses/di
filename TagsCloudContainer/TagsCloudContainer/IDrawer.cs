using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IDrawer<T>
    {
        void DrawItems(IEnumerable<ItemToDraw<T>> itemsToDraws);
    }
}
