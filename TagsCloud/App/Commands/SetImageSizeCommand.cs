using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetImageSizeCommand : ICommand
    {
        private readonly ImageHolder imageHolder;
        private readonly IImageSizeProvider imageSizeProvider;

        public SetImageSizeCommand(IImageSizeProvider imageSizeProvider, ImageHolder imageHolder)
        {
            this.imageSizeProvider = imageSizeProvider;
            this.imageHolder = imageHolder;
        }

        public string Category { get; }
        public string Name { get; } = "setsize";
        public string Description { get; }

        public void Execute(string[] args)
        {
            var imageSize = imageSizeProvider.ImageSize;
            imageSize.Width = int.Parse(args[0]);
            imageSize.Height = int.Parse(args[1]);
        }
    }
}