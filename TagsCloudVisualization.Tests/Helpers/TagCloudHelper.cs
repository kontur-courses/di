using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Configurations;

namespace TagsCloudVisualization.Tests.Helpers;

public static class TagCloudHelper
{
    public static Bitmap DrawTagCloudRectangles(List<Rectangle> rects, CloudConfiguration cloudConfiguration)
    {
        var imageWidth = cloudConfiguration.ImageSize.Width;
        var imageHeight = cloudConfiguration.ImageSize.Height;
            
        var bitmap = new Bitmap(imageWidth, imageHeight);
        using var gp = Graphics.FromImage(bitmap);
            
        gp.FillRectangle(new SolidBrush(cloudConfiguration.BackgroundColor), new Rectangle(0,0, imageWidth, imageHeight));
        gp.DrawRectangles(new Pen(cloudConfiguration.PrimaryColor), rects.ToArray());
            
        return bitmap;
    }
    
    public static List<Size> GenerateRectangleSizesRandom(
        int amount, int minWidth = 20, int maxWidth = 150, int minHeight = 20, int maxHeight = 100)
    {
        var rnd = new Random();
        var listSize = new List<Size>();
            
        for (var i = 0; i < amount; i++)
            listSize.Add(new Size(rnd.Next(minWidth, maxWidth), rnd.Next(minHeight, maxHeight)));

        return listSize;
    }
}