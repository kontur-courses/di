﻿using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IVisualizer
{
    Bitmap GetBitmap();
}