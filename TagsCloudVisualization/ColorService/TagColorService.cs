﻿using System.Drawing;

namespace TagsCloudVisualization.ColorService
{
    public class TagColorService : ITagColorService
    {
        private readonly Color color;

        public TagColorService(Color color) => this.color = color;

        public Color GetColor() => color;
    }
}