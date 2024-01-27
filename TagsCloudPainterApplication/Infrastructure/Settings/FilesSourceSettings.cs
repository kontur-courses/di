﻿using TagsCloudPainterApplication.Properties;

namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class FilesSourceSettings
{
    private string boringTextFilePath;

    public FilesSourceSettings(AppSettings settings)
    {
        BoringTextFilePath = @$"{Environment.CurrentDirectory}..\..\..\..\" + settings.boringTextFilePath;
    }

    public string BoringTextFilePath
    {
        get => boringTextFilePath;
        set => boringTextFilePath = value ?? boringTextFilePath;
    }
}