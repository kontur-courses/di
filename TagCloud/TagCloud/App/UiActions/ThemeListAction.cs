namespace TagCloud
{
    public class ThemeListAction : IUiAction
    {
        private readonly ThemeList themeList;

        public ThemeListAction(ThemeList themeList)
        {
            this.themeList = themeList;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Themes...";
        public string Description => "Themes choice";

        public void Perform()
        {
            CheckedListForm.For(themeList).ShowDialog();
        }
    }
}
