using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class TextReaderSelector : IService
{
    private readonly InputSettings settings;
    private readonly Lazy<FileReader> fileReader;
    private readonly Lazy<SimpleStringReader> stringReader;

    public TextReaderSelector(InputSettings settings, Lazy<FileReader> fileReader, Lazy<SimpleStringReader> stringReader)
    {
        this.settings = settings;
        this.fileReader = fileReader;
        this.stringReader = stringReader;
    }

    public ITextReader GetReader()
    {
        if (settings.UseFile)
            return fileReader.Value;
        return stringReader.Value;
    }
}