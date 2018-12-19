using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImagePerfomer
    {
        private readonly IDrawer drawer;
        private readonly IImageSaver saver;

        public ImagePerfomer(IDrawer drawer, IImageSaver saver)
        {
            this.drawer = drawer;
            this.saver = saver;
        }

        public void DrawAndSave()
        {
            drawer.Draw();
            saver.Save();
        }
    }
}