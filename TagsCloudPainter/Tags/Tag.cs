﻿namespace TagsCloudPainter.Tags;

public class Tag
{
    public Tag(string value, float fontSize, int count)
    {
        Value = value;
        FontSize = fontSize;
        Count = count;
    }

    public string Value { get; private set; }
    public float FontSize { get; private set; }
    public int Count { get; private set; }
}