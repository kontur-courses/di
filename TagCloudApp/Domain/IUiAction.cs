using TagCloudApp.Infrastructure;

namespace TagCloudApp.Domain;

public interface IUiAction
{
    MenuCategory Category { get; }
    string Name { get; }
    string Description { get; }
    void Perform();
}