using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IUiAction
{
    MenuCategory Category { get; }
    string Name { get; }
    string Description { get; }
    void Perform();
}