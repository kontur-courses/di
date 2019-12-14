using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TagsCloudContainer.UI.SettingsCommands
{
    public class ImageSizeSettingsCommand : SettingsCommand
    {
        public override string Name { get; } = "size";

        public override Result<IInitialSettings> TryChangeSettings(string[] arguments, IInitialSettings settings)
        {
            if (arguments.Length < 3)
                return Result.Fail<IInitialSettings>("Should be 2 arguments: width and height");
            if (!int.TryParse(arguments[1], out var width) || !int.TryParse(arguments[2], out var height))
                return Result.Fail<IInitialSettings>("Arguments should be integers");
            if (width <= 0 || height <= 0)
                return Result.Fail<IInitialSettings>("Width and height should be positive");
            var newSettings = (IInitialSettings) settings.Clone();
            newSettings.ImageSize = new Size(width, height);
            return Result.Ok(newSettings);
        }
    }
}
