using System.Configuration;

namespace TagsCloudCore.Common;

public static class AppSettings
{
    public static string PathToBoringWordsFilter => ConfigurationManager.AppSettings["BoringWordsFilterPath"]!;
}