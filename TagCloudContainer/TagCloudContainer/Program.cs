namespace TagCloudContainer;

public class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Settings());
    }
}