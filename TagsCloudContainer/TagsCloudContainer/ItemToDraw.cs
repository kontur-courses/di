namespace TagsCloudContainer
{
    public class ItemToDraw<T>: IItemToDraw<T>
    {
        public T Body { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public ItemToDraw(T body, int x, int y, int width, int height)
        {
            Body = body;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
