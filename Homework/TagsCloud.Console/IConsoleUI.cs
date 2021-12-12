using TagsCloudContainer;

namespace TagsCloud.Console
{
    public interface IConsoleUI
    {
        public void Run(IAppSettings appSettings, ITagCloudSettings tagCloudsettings);
    }
}