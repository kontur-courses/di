using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsCloudContainer.UI.SettingsCommands
{
    public abstract class SettingsCommand : ISettingsCommand
    {
        public abstract string Name { get; }

        public bool IsThisCommandInput(string input, out string[] arguments)
        {
            arguments = input.Split(' ');
            if (arguments.Length == 0 || arguments[0] != Name)
                return false;
            return true;
        }

        public abstract Result<IInitialSettings> TryChangeSettings(string[] arguments, IInitialSettings settings);
    }
}
