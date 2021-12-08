﻿using System.Collections.Generic;
using System.Drawing;
using TagsCloudDrawer.Drawer;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;

namespace TagsCloudDrawer.ImageCreator
{
    public class ImageCreator : IImageCreator
    {
        private readonly IDrawer _drawer;
        private readonly IImageSavior _savior;
        private readonly IImageSettingsProvider _settingsProvider;

        public ImageCreator(IDrawer drawer, IImageSavior savior,
            IImageSettingsProvider settingsProvider)
        {
            _drawer = drawer;
            _savior = savior;
            _settingsProvider = settingsProvider;
        }

        public void Create(string filename, IEnumerable<IDrawable> drawables)
        {
            var size = _settingsProvider.ImageSize;
            using var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(_settingsProvider.BackgroundColor);
            _drawer.Draw(graphics, bitmap.Size, drawables);
            _savior.Save(filename, bitmap);
        }
    }
}