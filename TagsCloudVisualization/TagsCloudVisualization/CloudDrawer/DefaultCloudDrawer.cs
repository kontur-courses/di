﻿using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.CloudDrawer;

public class DefaultCloudDrawer : ICloudDrawer
{
    private const string DefaultPath = "..\\..\\..\\Result.png";

    private Bitmap bitmap;
    private Graphics graphics;
    private Pen pen;

    public DefaultCloudDrawer(int windowWidth, int windowHeight, int borderWidth, Color borderColor)
    {
        bitmap = new Bitmap(windowWidth, windowHeight);
        graphics = Graphics.FromImage(bitmap);
        pen = new Pen(borderColor, borderWidth);
    }

    public void Draw(Dictionary<string, Rectangle> wordsInRect)
    {
        foreach (var (word, rect) in wordsInRect)
            graphics.DrawRectangle(pen, rect);
        SaveImage();
    }

    public void SaveImage()
    {
        bitmap.Save(DefaultPath, ImageFormat.Png);
    }
}