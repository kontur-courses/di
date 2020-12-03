using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetFontCommand : ICommand
    {
        private readonly IFontFamilyProvider fontFamilyProvider;

        public SetFontCommand(IFontFamilyProvider fontFamilyProvider)
        {
            this.fontFamilyProvider = fontFamilyProvider;
        }

        public string Name { get; } = "setfont";
        public string Description { get; } = "setfont      # setting font for tag cloud";

        public void Execute(string[] args)
        {
            fontFamilyProvider.FontFamily = new FontFamily(args[0]);
        }
    }
}