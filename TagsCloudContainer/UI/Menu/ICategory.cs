namespace TagsCloudContainer.UI.Menu
{
    public interface ICategory
    {
        public MenuItem[] Items { get; }
        public string Name { get; }

        public void ChooseMenuItem();
    }
}