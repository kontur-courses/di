namespace TagsCloudContainer.Core.Drawer.Interfaces
{
    public interface IRectangleLayout
    {
        public void DrawLayout();

        public void SaveLayout();

        public void PlaceWords(Dictionary<string, int> words);
    }
}