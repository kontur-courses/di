﻿using System.Drawing;
using TagsCloud.Visualization.FontFactory;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.WordsSizeService
{
    public interface IWordsSizeService
    {
        Size CalculateSize(Word word, FontDecorator font);
    }
}