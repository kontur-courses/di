﻿using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class DrawingCloudCreator : ICloudCreator
{
    private readonly ICloudDrawer drawer;
    private readonly ICloudLayouter layouter;

    public DrawingCloudCreator(ICloudDrawer drawer, ICloudLayouter layouter)
    {
        this.drawer = drawer;
        this.layouter = layouter;
    }

    public Bitmap CreateTagCloud(IEnumerable<ITag> tags)
    {
        var tagsArray = tags.ToArray();
        var maxCount = tagsArray.Max(t => t.Weight);
        var minCount = tagsArray.Min(t => t.Weight);
        var wordFontSizeAndLocation = tagsArray.Select(t =>
        {
            var fontsize = GetFontSize(t.Weight, minCount, maxCount);
            return (t.Text, fontsize, GetPoint(t.Text, fontsize));
        });
        return drawer.Draw(wordFontSizeAndLocation);
    }

    private Point GetPoint(string word, int fontsize)
    {
        using var font = new Font(drawer.FontFamily, fontsize);
        return layouter.PutNextRectangle(drawer.Graphics.MeasureString(word, font).ToSize()).Location;
    }

    private int GetFontSize(int count, int minCount, int maxCount)
    {
        var maxSize = drawer.MaxFontSize;
        var minSize = drawer.MinFontSize;
        return - (int)((double)(minSize * maxCount - minSize * count + maxSize * count - maxSize * minCount) /
                     (minCount - maxCount));
    }
}