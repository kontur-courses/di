namespace TagsCloudContainer.UI.Menu
{
    public class MenuCreator
    {
        public MainMenu Menu { get; }
        public MenuCreator(IUiAction[] actions)
        {
            Menu = actions.GetMenu();
        }
    }
}