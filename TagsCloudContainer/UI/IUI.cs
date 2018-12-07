namespace TagsCloudContainer.UI
{
    public interface IUI
    {
        (string, string) RetrievePaths(string[] args);
    }
}