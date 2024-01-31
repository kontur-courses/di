using TagCloudDi.Drawer;

namespace TagCloudDi.Applications
{
    public class ConsoleApplication(IDrawer drawer, Settings settings) : IApplication
    {
        public void Run()
        {
            var image = drawer.GetImage();
            image.Save($"{settings.SavePath}.{settings.ImageFormat.ToLower()}", settings.GetFormat());
            Console.WriteLine($"Saved to {settings.SavePath + '.' + settings.ImageFormat.ToLower()}");
        }
    }
}
