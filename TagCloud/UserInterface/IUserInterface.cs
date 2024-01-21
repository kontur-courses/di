using TagCloud.Settings;

namespace TagCloud.UserInterface;

public interface IUserInterface
{
    void Run(IAppSettings appSettings);
}