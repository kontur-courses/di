using System.Collections.Generic;

namespace TagCloud
{
    public class BoringWordsList : IItemList<string>
    {
        public HashSet<string> SelectedItems { get; set; }

        public HashSet<string> AllItems { get; }

        public BoringWordsList(HashSet<string> selectedItems, HashSet<string> allItems)
        {
            SelectedItems = selectedItems;
            AllItems = allItems;
        }
    }
}
