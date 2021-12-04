using TagCloudContainer.Infrastructure.Common;

namespace TagCloudContainer.App.UI;

public interface IUserInterface
{
    public void Run(IAppSettings settings);
}