using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.UI.SettingsCommands
{
    public class InputFileSettingsCommand : SettingsCommand
    {
        public override string Name { get; } = "input";

        public override Result<IInitialSettings> TryChangeSettings(string[] arguments, IInitialSettings settings)
        {
            if (arguments.Length < 2)
                return Result.Fail<IInitialSettings>("Should be 1 argument: input path");
            var path = arguments[1];
            if (!File.Exists(path))
                return Result.Fail<IInitialSettings>($"File {path} not exists");
            var newSettings = (IInitialSettings)settings.Clone();
            newSettings.InputFilePath = path;
            return Result.Ok(newSettings);
        }
    }
}
