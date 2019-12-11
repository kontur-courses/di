namespace TagCloud
{
    public class FilterListAction : IUiAction
    {
        private readonly FilterList filterList;

        public FilterListAction(FilterList filterList)
        {
            this.filterList = filterList;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Filters...";
        public string Description => "Filters choice";

        public void Perform()
        {
            CheckedListForm.For(filterList).ShowDialog();
        }
    }
}
