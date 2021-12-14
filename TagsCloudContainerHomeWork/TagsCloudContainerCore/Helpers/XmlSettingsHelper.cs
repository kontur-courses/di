using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using TagsCloudContainerCore.Console;

namespace TagsCloudContainerCore.Helpers;

public static class XmlSettingsHelper
{
    private static readonly XmlSerializer Serializer = new(typeof(LayoutSettings));

    public static void CreateSettingsFile()
    {
        var settings = new LayoutSettings
        {
            PathToExcludedWords = null,
            BackgroundColor = "FFFFFF",
            FontName = "Arial",
            FontColor = "000000",
            MinAngle = 5,
            Step = 10,
            PictureSize = new Size(1080, 720),
            MaxFontSize = 70
        };

        using var fileWriter = new StreamWriter("./TagsCloudSettings.xml");
        Serializer.Serialize(fileWriter, settings);
    }

    public static void CreateSettingsFile(LayoutSettings settings)
    {
        using var fileWriter = new StreamWriter("./TagsCloudSettings.xml");
        Serializer.Serialize(fileWriter, settings);
    }

    public static LayoutSettings GetLayoutSettings()
    {
        using var fileReader = new StreamReader("./TagsCloudSettings.xml");
        return (LayoutSettings)Serializer.Deserialize(fileReader);
    }
}