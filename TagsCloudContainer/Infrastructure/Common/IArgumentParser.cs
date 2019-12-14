namespace TagsCloudContainer.Infrastructure.Common
{
    public interface IArgumentParser
    {
        WordSetting GetWordSetting(string[] args);
        ImageSetting GetImageSetting(string[] args);
        string GetPath(string[] args);
    }
}