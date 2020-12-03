using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly ImageHolder imageHolder;

        public SaveCommand(ImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public string Name { get; } = "save";
        public string Description { get; } = "save <fileName>      # save tag cloud image";

        public void Execute(string[] args)
        {
            imageHolder.SaveImage(args[0]);
        }
    }
}