using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetColorCommand : ICommand
    {
        private readonly IImageColorProvider colorSettingsProvider;

        public SetColorCommand(IImageColorProvider colorSettingsProvider)
        {
            this.colorSettingsProvider = colorSettingsProvider;
        }

        public string Name { get; } = "setcolor";
        public string Description { get; }

        public void Execute(string[] args)
        {
            var colors = args.Select(x => char.ToUpper(x[0]) + x.Substring(1).ToLower());
            foreach (var color in colors) colorSettingsProvider.AddColor(Color.FromName(color));
        }
    }
}