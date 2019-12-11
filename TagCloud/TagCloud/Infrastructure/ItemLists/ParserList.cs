using System.Collections.Generic;

namespace TagCloud
{
    public class ParserList : IItemList<IParser>
    {
        public HashSet<IParser> SelectedItems { get; set; }

        public HashSet<IParser> AllItems { get; }

        public ParserList(HashSet<IParser> selectedItems, HashSet<IParser> allItems)
        {
            SelectedItems = selectedItems;
            AllItems = allItems;
        }
    }
}
