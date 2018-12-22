using System.Collections.Generic;


namespace TagsCloudContainer
{
    internal interface ILayouter<T>
    {
        IEnumerable<ItemToDraw<T>> GetItemsToDraws();
    }
}
