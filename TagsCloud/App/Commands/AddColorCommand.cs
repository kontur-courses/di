using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class AddColorCommand : ICommand
    {
        private readonly IImageColorProvider colorSettingsProvider;

        public AddColorCommand(IImageColorProvider colorSettingsProvider)
        {
            this.colorSettingsProvider = colorSettingsProvider;
        }

        public string Name { get; } = "addcolor";
        public string Description { get; } = "addcolor params <color>      # Adding colors for cloud tag";

        public void Execute(string[] args)
        {
            var colors = args.Select(x => char.ToUpper(x[0]) + x.Substring(1).ToLower());
            foreach (var color in colors) colorSettingsProvider.AddColor(Color.FromName(color));
        }
    }
}