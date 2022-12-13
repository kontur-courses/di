using System;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Gui;

public interface IImageListProvider
{
    void AddImageBits(byte[] imageBytes);
}

public class GuiGraphicsProviderSettings : GraphicsProviderSettings
{
    public int Width { get; set; } = 1000;

    public int Height { get; set; } = 1000;

    public bool Save { get; set; }

    public string SavePath { get; set; } =
        Path.Combine(AppContext.BaseDirectory, "assets");
}