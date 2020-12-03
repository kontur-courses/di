using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetImageSizeCommand : ICommand
    {
        private readonly IImageSizeProvider imageSizeProvider;

        public SetImageSizeCommand(IImageSizeProvider imageSizeProvider)
        {
            this.imageSizeProvider = imageSizeProvider;
        }

        public string Name { get; } = "setsize";
        public string Description { get; } = "setsize <width> <height>      # setting tag cloud size";

        public void Execute(string[] args)
        {
            var imageSize = imageSizeProvider.ImageSize;
            imageSize.Width = int.Parse(args[0]);
            imageSize.Height = int.Parse(args[1]);
        }
    }
}