namespace TagsCloudContainer.Infrastucture.Visualization
{
    public interface IDrawer
    {
        public void Draw(List<TextRectangle> rectangles);
    }
}