namespace TagsCloudApp
{
    public interface IObjectParser<T>
    {
        T Parse(string value);
    }
}