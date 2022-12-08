using MyStemWrapper.Domain.Settings;

namespace MyStemWrapper.Infrastructure;

public static class MyStemSettingsExtensions
{
    public static bool CheckCorrectAppPath(this MyStemSettings settings)
    {
        return File.Exists(settings.MyStemAppPath);
    }

    public static string GetLaunchArgsToString(this MyStemSettings settings)
    {
        if (settings.LaunchOptions is null)
            return string.Empty;
        var args = settings.LaunchOptions
            .Select(option => option.ToStringArg())
            .Prepend(settings.Encoding.ToStringArg())
            .Prepend(settings.OutputFormat.ToStringArg());
        return string.Join(" ", args);
    }
}