namespace TagsCloudContainer.Core.Drawer.Interfaces
{
    internal interface IRectangleLayout
    {
        public void DrawLayout();

        public void SaveLayout();

        public void PlaceWords(Dictionary<string, int> words);
    }
}
