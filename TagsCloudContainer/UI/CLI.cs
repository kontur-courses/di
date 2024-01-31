using System.Diagnostics;

namespace TagsCloudContainer.UI;

public class CLI : IUI
{
    public void View(string output)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = output,
            UseShellExecute = true
        }); 
    }
}