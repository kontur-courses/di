using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ConsoleUI : IUI
{
    private ImageSettingsAction imageSettingsAction;
    private CloudSettingsAction cloudSettingsAction;
    private GenerateImageAction generateImageAction;

    public ConsoleUI(
        ImageSettingsAction imageSettingsAction,
        CloudSettingsAction cloudSettingsAction,
        GenerateImageAction generateImageAction)
    {
        this.imageSettingsAction = imageSettingsAction;
        this.cloudSettingsAction = cloudSettingsAction;
        this.generateImageAction = generateImageAction;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1 - Image settings");
            Console.WriteLine("2 - Cloud settings");
            Console.WriteLine("3 - Generate image");
            Console.WriteLine("4 - Exit");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            switch (answer.KeyChar)
            {
                case '1':
                    imageSettingsAction.Handle();
                    break;
                case '2':
                    cloudSettingsAction.Handle();
                    break;
                case '3':
                    generateImageAction.Handle();
                    break;
                default:
                    return;
            }
        }
    }
}
