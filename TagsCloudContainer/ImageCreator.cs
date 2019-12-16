using System.Drawing;
using TagsCloudContainer.Infrastructure.Common;

namespace TagsCloudContainer
{
    public class ImageCreator
    {
        private Compositor compositor;
        private ImageSetting setting;
        private IDrawer drawer;
        
        public ImageCreator(ImageSetting setting, Compositor compositor, IDrawer drawer)
        {
            this.setting = setting;
            this.compositor = compositor;
            this.drawer = drawer;
        }

        public void Save()
        {
            var words = compositor.Composite();
            var image = drawer.Draw(setting, words);

            image.Save($"{setting.Name}.{setting.Format}");
            image.Dispose();
        }
    }
}