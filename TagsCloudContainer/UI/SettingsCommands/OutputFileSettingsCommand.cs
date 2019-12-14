using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.UI.SettingsCommands
{
    public class OutputFileSettingsCommand : SettingsCommand
    {
        public override string Name { get; } = "output";

        public override Result<IInitialSettings> TryChangeSettings(string[] arguments, IInitialSettings settings)
        {
            if (arguments.Length < 2)
                return Result.Fail<IInitialSettings>("Should be 1 argument: output path");
            var path = arguments[1];
            var newSettings = (IInitialSettings)settings.Clone();
            newSettings.OutputFilePath = path;
            return Result.Ok(newSettings);
        }
    }
}
