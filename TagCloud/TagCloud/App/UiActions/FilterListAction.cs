namespace TagCloud
{
    public class FilterListAction : IUiAction
    {
        private readonly IFilter[] filters;

        public FilterListAction(IFilter[] filters)
        {
            this.filters = filters;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Filters...";
        public string Description => "Filters choice";

        public void Perform()
        {
            CheckedListForm.For(filters).ShowDialog();
        }
    }
}
