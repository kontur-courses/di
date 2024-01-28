namespace TagsCloudContainer.UI;

public interface IUI
{
    ApplicationArguments Setup(string[] args);
    void View(string output);
}