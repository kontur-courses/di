using System.Collections.Generic;

namespace TagCloud
{
    public class ThemeList : IItemList<ITheme>
    {
        public HashSet<ITheme> SelectedItems { get; set; }

        public HashSet<ITheme> AllItems { get; }

        public ThemeList(HashSet<ITheme> selectedItems, HashSet<ITheme> allItems)
        {
            SelectedItems = selectedItems;
            AllItems = allItems;
        }
    }
}
