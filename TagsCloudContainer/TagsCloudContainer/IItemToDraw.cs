namespace TagsCloudContainer
{
    public interface IItemToDraw<T>
    {
        T Body { get; }
        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }
    }
}
