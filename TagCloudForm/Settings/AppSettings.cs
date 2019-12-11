namespace TagCloudForm.Settings
{
    public class AppSettings : IDirectoryProvider
    {
        public string Directory { get; } = @"..\..\..\TagCloud\.";
        public const string FormName = "Tag cloud";
    }
}