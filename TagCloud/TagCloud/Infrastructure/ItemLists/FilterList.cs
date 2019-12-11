using System.Collections.Generic;

namespace TagCloud
{
    public class FilterList : IItemList<IFilter>
    {
        public HashSet<IFilter> SelectedItems { get; set; }

        public HashSet<IFilter> AllItems { get; }

        public FilterList(HashSet<IFilter> selectedItems, HashSet<IFilter> allItems)
        {
            SelectedItems = selectedItems;
            AllItems = allItems;
        }
    }
}
