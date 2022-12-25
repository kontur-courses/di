﻿using TagCloudGUI.Settings;

namespace TagCloudGUI.Interfaces
{
    public interface IImageSettingsProvider
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}
