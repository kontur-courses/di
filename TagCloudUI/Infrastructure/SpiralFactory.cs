﻿using System.Drawing;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure
{
    public class SpiralFactory : ISpiralFactory
    {
        private readonly AppSettings settings;

        public SpiralFactory(AppSettings settings)
        {
            this.settings = settings;
        }

        public ISpiral Create()
        {
            var center = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
            return new ArchimedeanSpiral(center);
        }
    }
}