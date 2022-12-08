using MyStemWrapper.Domain.Settings;

namespace MyStemWrapper.Infrastructure;

public static class MyStemEnumExtensions
{
    internal static string ToStringArg(this MyStemLaunchOption launchOption) =>
        launchOption switch
        {
            MyStemLaunchOption.LinearMode => "-n",
            MyStemLaunchOption.CopyEverything => "-c",
            MyStemLaunchOption.OnlyVocabulary => "-w",
            MyStemLaunchOption.MissOriginalForm => "-l",
            MyStemLaunchOption.AddGrammarInfo => "-i",
            MyStemLaunchOption.JoinSingleLemmaWordsForms => "-g",
            MyStemLaunchOption.AddSentenceEndMarker => "-s",
            MyStemLaunchOption.ApplyContextualDishomonymization => "-d",
            _ => throw new ArgumentOutOfRangeException(nameof(launchOption), $"Unsupported option: {launchOption}")
        };

    public static string ToStringArg(this MyStemOutputFormat format) =>
        format switch
        {
            MyStemOutputFormat.Json => "--format json",
            MyStemOutputFormat.Xml => "--format xml",
            MyStemOutputFormat.Text => "--format text",
            _ => throw new ArgumentOutOfRangeException(nameof(format), $"Unsupported format: {format}")
        };

    public static string ToStringArg(this MyStemEncoding encoding) =>
        encoding switch
        {
            MyStemEncoding.Cp866 => "-e cp866",
            MyStemEncoding.Cp1251 => "-e cp1251",
            MyStemEncoding.Koi8R => "-e koi8-r",
            MyStemEncoding.Utf8 => "-e utf-8",
            _ => throw new ArgumentOutOfRangeException(nameof(encoding), $"Unsupported encoding: {encoding}")
        };
}