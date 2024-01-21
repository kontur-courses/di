namespace TagsCloudConsoleUI;

public static class Program
{
    public static void Main()
    {
        try
        {
            new ConsoleUi().StartUi();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}