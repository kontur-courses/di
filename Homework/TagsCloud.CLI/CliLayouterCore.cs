using TagsCloud.Visualization.ImagesSavior;
using TagsCloud.Visualization.LayouterCores;

namespace TagsCloud.Words
{
    public class CliLayouterCore
    {
        private readonly IImageSavior imageSavior;
        private readonly ILayouterCore layouterCore;

        public CliLayouterCore(
            ILayouterCore layouterCore,
            IImageSavior imageSavior)
        {
            this.layouterCore = layouterCore;
            this.imageSavior = imageSavior;
        }

        public void Run()
        {
            using var image = layouterCore.GenerateImage();

            imageSavior.Save(image);
        }
    }
}