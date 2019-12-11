using System.Collections.Generic;

namespace TagCloud
{
    public class SpeechPartList : IItemList<SpeechPart>
    {
        public HashSet<SpeechPart> SelectedItems { get; set; }

        public HashSet<SpeechPart> AllItems { get; }

        public SpeechPartList(HashSet<SpeechPart> selectedItems, HashSet<SpeechPart> allItems)
        {
            SelectedItems = selectedItems;
            AllItems = allItems;
        }
    }
}
