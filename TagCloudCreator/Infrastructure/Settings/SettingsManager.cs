using TagCloudCreator.Domain.Settings;

namespace TagCloudCreator.Infrastructure.Settings;

public class SettingsManager
{
    private const string SettingsFilename = "app.settings";
    private readonly IObjectSerializer _serializer;
    private readonly IBlobStorage _storage;

    public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
    {
        _serializer = serializer;
        _storage = storage;
    }

    public AppSettings Load()
    {
        try
        {
            var data = _storage.Get(SettingsFilename);
            if (data is not null)
                return _serializer.Deserialize<AppSettings>(data)!;

            var defaultSettings = CreateDefaultSettings();
            Save(defaultSettings);
            return defaultSettings;
        }
        catch (Exception)
        {
            return CreateDefaultSettings();
        }
    }

    private static AppSettings CreateDefaultSettings() =>
        new()
        {
            ImagePath = ".",
            WordsPath = ".",
            ImageSettings = new ImageSettings
            {
                Width = 800,
                Height = 600
            }
        };

    public void Save(AppSettings settings)
    {
        _storage.Set(SettingsFilename, _serializer.Serialize(settings));
    }
}