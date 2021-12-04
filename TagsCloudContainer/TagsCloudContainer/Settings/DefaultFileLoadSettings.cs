namespace TagsCloudContainer.Settings
{
    public interface IFileLoadSettings
    {
        string FileName { get; set; }
    }

    public class DefaultFileLoadSettings : IFileLoadSettings
    {
        public string FileName { get; set; } = "input.txt";
    }
}