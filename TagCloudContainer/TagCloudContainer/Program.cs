using Autofac;
using TagCloudContainer.Forms;

namespace TagCloudContainer;

public class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        using (var scope = Register.Registry().BeginLifetimeScope())
            Application.Run(scope.Resolve<Settings>());
    }
}