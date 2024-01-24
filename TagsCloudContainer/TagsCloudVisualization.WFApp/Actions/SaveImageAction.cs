﻿using TagsCloudVisualization.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageHolder imageHolder;

    public SaveImageAction(IImageHolder imageHolder)
    {
        this.imageHolder = imageHolder;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Сохранить...";
    public string Description => "Сохранить изображение в файл";

    public void Perform()
    {
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = "/",
            DefaultExt = "png",
            FileName = "tagCloud.png",
            Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp" 
        };
        var res = dialog.ShowDialog();
        if (res == DialogResult.OK)
            imageHolder.SaveImage(dialog.FileName);
    }
}