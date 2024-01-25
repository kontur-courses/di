using TagCloudDi.Layouter;

namespace TagCloudDi.Applications
{
    public class ConsoleApplication(Drawer drawer, Settings settings) : IApplication
    {
        public void Run()
        {
            var image = drawer.GetImage();
            image.Save(settings.SavePath);
            Console.WriteLine($"Saved to {settings.SavePath}");
        }
    }
}
