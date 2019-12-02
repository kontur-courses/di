﻿using TagCloud;

namespace TagCloudForm.Settings
{
    public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
    {
        public string ImagesDirectory { get; set; } = ".";
        public ImageSettings ImageSettings { get; set; }
    }
}