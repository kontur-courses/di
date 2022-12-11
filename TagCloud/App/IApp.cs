using TagCloud.AppConfig;

namespace TagCloud.App
{
    public interface IApp
    {
        void Run(IAppConfig appConfig);
    }
}
