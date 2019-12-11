namespace TagCloud
{
    public class ThemeListAction : IUiAction
    {
        private readonly ITheme[] themes;

        public ThemeListAction(ITheme[] themes)
        {
            this.themes = themes;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Themes...";
        public string Description => "Themes choice";

        public void Perform()
        {
            CheckedListForm.For(themes).ShowDialog();
        }
    }
}
