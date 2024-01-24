using TagCloud.AppSettings;

namespace TagCloud.UserInterface;

public interface IUserInterface
{
    void Run(IAppSettings appSettings);
}