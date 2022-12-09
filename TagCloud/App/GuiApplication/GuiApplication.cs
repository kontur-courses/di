using Autofac;

namespace App.GuiApplication;

public class GuiApplication : IApplication
{
    public void Run(IContainer container, IEnumerable<string> args)
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new TagCloudForm());
    }
}