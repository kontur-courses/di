namespace TagsCloudContainer.UI.Menu
{
    public interface IMainMenu
    {
        public ConsoleCategory[] Categories { get; }

        public void ChooseCategory();
    }
}