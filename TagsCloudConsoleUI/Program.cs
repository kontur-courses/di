using TagsCloudContainer.Common;

namespace TagsCloudConsoleUI;

public static class Program
{
    public static void Main()
    {
        try
        {
            new ConsoleUi(DiContainerBuilder.BuildContainer()).StartUi();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}