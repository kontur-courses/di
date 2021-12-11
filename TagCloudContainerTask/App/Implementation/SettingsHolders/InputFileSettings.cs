using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class InputFileSettings : IInputFileSettingsHolder
    {
        public string InputFileName { get; set; } = "words.txt";
    }
}