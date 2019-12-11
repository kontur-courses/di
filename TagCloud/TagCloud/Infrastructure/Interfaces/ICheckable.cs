namespace TagCloud
{
    public interface ICheckable
    {
        bool IsChecked { get; set; }
        string Name { get; }
    }
}
